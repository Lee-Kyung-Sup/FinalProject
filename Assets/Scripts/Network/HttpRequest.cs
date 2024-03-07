using System;
using Constants;
using UnityEngine;

public class HttpRequest
{
    public string Url { get; private set; }
    public ContentsType ContentsType { get; private set; }
    public string JsonData { get; private set; }
    public WWWForm formData { get; private set; }
    public HTTPMethod HttpMethod { get; private set; }
    public Action<HttpResult> CompleteCallback { get; private set; }
    
    
    // Set Http Data
    private HttpRequest(string url, HTTPMethod httpMethod, ContentsType contentsType, Action<HttpResult> completeCallback)
    {
        Url = url;
        HttpMethod = httpMethod;
        ContentsType = contentsType;
        CompleteCallback = completeCallback;
    }

    private void SetJsonData(string data)
    {
        JsonData = data;
    }

    private void SetFormData(WWWForm wwwForm)
    {
        formData = wwwForm ?? new WWWForm();
    }
    
    
    // Send Data
    public static void Send(string url, WWWForm wwwForm = null, bool acceptCors = true, HTTPMethod httpMethod = HTTPMethod.POST, Action<HttpResult> completeCallback = null)
    {
        var reqObj =  new HttpRequest(url, httpMethod, ContentsType.None, completeCallback);
        reqObj.SetFormData(wwwForm);
        
        ServerManager.Instance.Request(reqObj);
    }
    
    public static void Send<T>(string url, T sendData, bool acceptCors = true, HTTPMethod httpMethod = HTTPMethod.POST, Action<HttpResult> completeCallback = null)
    {
        var reqObj =  new HttpRequest(url, httpMethod ,ContentsType.Json , completeCallback);
        var jsonData = JsonUtility.ToJson(sendData);
        reqObj.SetJsonData(jsonData);
        
        ServerManager.Instance.Request(reqObj);
    }
}