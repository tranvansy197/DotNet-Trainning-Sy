using System.Net;

namespace App.Api.exception;

public class BusinessException : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    public BusinessException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}