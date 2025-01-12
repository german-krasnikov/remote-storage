using System.Net;

public sealed class NotFoundMiddleware : IMiddleware
{
    public ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response)
    {
        response.StatusCode = (int) HttpStatusCode.NotFound;
        response.WriteText("Error: resource not Found");
        return new ValueTask<bool>(true);
    }
}