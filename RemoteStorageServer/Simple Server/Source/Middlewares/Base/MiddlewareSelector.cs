
using System.Net;

public sealed class MiddlewareSelector : IMiddleware
{
    private readonly IReadOnlyList<IMiddleware> _middlewares;

    public MiddlewareSelector(params IMiddleware[] middlewares)
    {
        _middlewares = middlewares;
    }

    public async ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response)
    {
        for (int i = 0, count = _middlewares.Count; i < count; i++)
        {
            IMiddleware middleware = _middlewares[i];
            if (await middleware.Handle(request, response))
                return true;
        }

        return false;
    }
}