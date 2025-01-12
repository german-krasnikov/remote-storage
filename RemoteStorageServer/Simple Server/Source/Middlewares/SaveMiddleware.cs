using System.Net;

public sealed class SaveMiddleware : IMiddleware
{
    private readonly Repository _repository;

    public SaveMiddleware(Repository repository)
    {
        _repository = repository;
    }

    public ValueTask<bool> Handle(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (!request.IsPut() || !request.IsAbsolutePath("/save"))
            return new ValueTask<bool>(false);

        this.HandleInternal(request, out HttpStatusCode status, out string message);

        response.StatusCode = (int) status;
        response.WriteText(message);
        return new ValueTask<bool>(true);
    }

    private void HandleInternal(HttpListenerRequest request, out HttpStatusCode status, out string message)
    {
        if (!request.HasEntityBody)
        {
            status = HttpStatusCode.BadRequest;
            message = "Error: expected content in request body!";
            return;
        }
        
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

        string json = request.ReadText();
        _repository.SetContent(version, json);

        status = HttpStatusCode.OK;
        message = $"Version {version} saved successfully!";
    }
}