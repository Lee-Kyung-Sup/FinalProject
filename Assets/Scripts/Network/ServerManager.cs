using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Constants;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : SingletoneBase<ServerManager>
{
    public void Request(HttpRequest request)
    {
	    StartCoroutine(HttpProcess(request));
    }
    
    private IEnumerator HttpProcess(HttpRequest httpRequest)
	{
		using (UnityWebRequest request = CreateWebRequest(httpRequest))
		{
#if UNITY_EDITOR
			Debug.Log($"HttpProcess req : <color=green>{request.uri}</color>");
#endif
			
			yield return request.SendWebRequest();
			
			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
			{
				Debug.Log($"HttpProcess Error : <color=red>{request.error}</color>");
				httpRequest
					.CompleteCallback?
					.Invoke(new HttpResult(false));
			}
			else
			{
#if UNITY_EDITOR
				Debug.Log($"HttpProcess Recv : <color=green>{request.downloadHandler.text}</color>");
#endif
				httpRequest
					.CompleteCallback?
					.Invoke(new HttpResult(true, request.downloadHandler.text));
			}
		}
	}

	private UnityWebRequest CreateWebRequest(HttpRequest httpRequest)
	{
		UnityWebRequest unityWebRequest = null;

		switch (httpRequest.HttpMethod)
		{
			case HTTPMethod.GET:
				unityWebRequest = CreateGetRequest(httpRequest);
				break;
			
			case HTTPMethod.POST:
				unityWebRequest = CreatePostRequest(httpRequest);
				break;
		}		
		
		return unityWebRequest;
	}

	private UnityWebRequest CreateGetRequest(HttpRequest httpRequest)
	{
		return UnityWebRequest.Get(httpRequest.Url);
	}

	private UnityWebRequest CreatePostRequest(HttpRequest httpRequest)
	{
		UnityWebRequest unityWebRequest = null;

		switch (httpRequest.ContentsType)
		{
			case ContentsType.None:
				unityWebRequest = UnityWebRequest.Post(httpRequest.Url, httpRequest.formData);
				break;
			
			case ContentsType.Json:
				unityWebRequest = UnityWebRequest.PostWwwForm(httpRequest.Url, httpRequest.JsonData);
				unityWebRequest.SetRequestHeader("Content-Type", "application/json");
				
				byte[] sendData = new UTF8Encoding().GetBytes(httpRequest.JsonData);
				unityWebRequest.uploadHandler = new UploadHandlerRaw(sendData);
				break;
		}		
		
		return unityWebRequest;
	}
}