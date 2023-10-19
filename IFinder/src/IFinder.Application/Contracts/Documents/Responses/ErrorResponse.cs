using System.Net;

namespace IFinder.Application.Contracts.Documents.Responses
{
    public sealed class ErrorResponse
    {
        public string? Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public ErrorResponse(HttpStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}