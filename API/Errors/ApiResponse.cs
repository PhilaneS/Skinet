using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageFroStatusCode(statusCode);
        }      

        public int StatusCode { get; set; }
        public string Message { get; set; }

         private string GetDefaultMessageFroStatusCode(int statusCode)
        {
            string Msg = string.Empty;

             switch(statusCode)
            {
                case 400: 
                Msg = "A bad request, you have made";
                break;
                
                case 401: 
                Msg = "Authorized, you are not";
                break;
                
                case 404:
                Msg = "Resouce Found, it was not";
                break;

                case 500:
                Msg = "Errors are the dark path side, errors lead to anger";
                break;                 
            };
           return Msg;
        }
    }
}