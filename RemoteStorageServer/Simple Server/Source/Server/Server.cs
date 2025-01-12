using System.Net;
using Zenject;

public sealed class Server : IInitializable, IDisposable
{
    private readonly string _url;
    private readonly int _port;
    
    private readonly HttpListener _listener = new();
    private readonly IMiddleware _middleware;

    public Server(string url, int port, IMiddleware middleware)
    {
        _url = url;
        _port = port;
        _middleware = middleware;
    }

    public void Initialize()
    {
        _listener.Prefixes.Add($"{_url}:{_port}/");
        _listener.Start();
        Task.Run(Listen);
    }

    public void Dispose()
    {
        _listener.Stop();
        _listener.Close();
    }

    private async void Listen()
    {
        Console.WriteLine($"Server started... {_url}:{_port}");
        
        while (_listener.IsListening)
        {
            HttpListenerContext context = await _listener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
               
            await _middleware.Handle(request, response);
            response.Close();
        }
    }
}