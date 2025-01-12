using System.Net;

public sealed class LoadMiddleware : IMiddleware
{
    private readonly Repository _repository;

    public LoadMiddleware(Repository repository)
    {
        _repository = repository;
    }

    public ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (!request.IsGet() || !request.IsAbsolutePath("/load"))
            return new ValueTask<bool>(false);

        this.HandleInternal(request, out HttpStatusCode status, out string message);

        response.StatusCode = (int) status;
        response.WriteText(message);
        return new ValueTask<bool>(true);
    }

    private void HandleInternal(HttpListenerRequest request, out HttpStatusCode status, out string message)
    {
        string? rawVersion = request.QueryString["version"];
        if (string.IsNullOrEmpty(rawVersion))
        {
            status = HttpStatusCode.BadRequest;
            message = "Error: 'version' parameter is not specified!";
            return;
        }

        if (!int.TryParse(rawVersion, out int version))
        {
            status = HttpStatusCode.BadRequest;
            message = "Error: 'version' parameter is not a number!";
            return;
        }

        if (!_repository.TryGetContent(version, out string content))
        {
            status = HttpStatusCode.NoContent;
            message = $"Error: version of game {version} doesn't exist!";
            return;
        }

        status = HttpStatusCode.OK;
        message = content;
    }
}