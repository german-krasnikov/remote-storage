using System.Net;

public sealed class MiddlewareSequence : IMiddleware
{
    private readonly IReadOnlyList<IMiddleware> _middlewares;

    public MiddlewareSequence(params IMiddleware[] middlewares)
    {
        _middlewares = middlewares;
    }

    public async ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response)
    {
        foreach (IMiddleware middleware in _middlewares)
            if (!await middleware.Handle(request, response))
                return false;
        
        return true;
    }
}