using UnityEngine;

public class HttpResult
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }

    public string ResultString  { get; private set; }

    public HttpResult() { }

    public HttpResult(bool isSuccess, string resultString = "", int statusCode = 0)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        
        ResultString = resultString;
    }
    
    public T GetResponseData<T>()
    {
        if (string.IsNullOrEmpty(ResultString))
            return default(T);

        return JsonUtility.FromJson<T>(ResultString);
    }
}