using System.Net;

public interface IMiddleware
{
    ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response);
}



