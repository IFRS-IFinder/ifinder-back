using System.Net;

namespace IFinder.Application.Contracts.Documents.Responses
{
    public class Response
    {
        public ErrorResponse? Error { get; set; }

        public Response() { }

        public Response(HttpStatusCode statusCode, string? message) =>
            Error = new ErrorResponse(statusCode, message);

        public bool IsErrorStatusCode() => Error is not null;

        public int GetErrorCode() => (int)Error?.StatusCode;

        public string? GetErrorMessage() => Error?.Message;
        
    }

    public class Response<T> : Response
    {
        public T? Data { get; private set; }

        public Response(T data) => Data = data;

        public Response(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }
    }
}