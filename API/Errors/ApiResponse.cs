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
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resouce Found, it was not",
                500 => "Errors are the dark path side, errors lead to anger",
            };
           // return v
        }
    }
}