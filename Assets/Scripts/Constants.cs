namespace Constants
{
    public enum HTTPMethod
    {
        NONE,
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum ContentsType
    {
        None,
        Json,
        Urlencoded,
        Upload
    }
    
    public class ServerUrl
    {

#if DEV
        private  const string domain = "http://ec2-43-201-76-35.ap-northeast-2.compute.amazonaws.com:3000/";
#elif QA
        private  const string domain = "http://ec2-43-201-76-35.ap-northeast-2.compute.amazonaws.com:3000/";
#elif LIVE
        private  const string domain = "http://ec2-43-201-76-35.ap-northeast-2.compute.amazonaws.com:3000/";
#else
        private const string domain = "http://ec2-43-201-76-35.ap-northeast-2.compute.amazonaws.com:3000/";
#endif

        // Auth
        public const string signUp = domain + "auth/signUp";
        public const string login = domain + "auth/signIn";
        
        // Room
        public const string roomList = domain + "room/setting";
    }
    
    public enum eCode
    {
        Success = 200,
        Unauthorized = 401,
        NotFound = 404, 
        Error = 405, 
    }
}