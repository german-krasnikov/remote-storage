using Zenject;

public sealed class ServerInstaller : Installer
{
    private const string url = "http://127.0.0.1";
    private const int port = 8888;

    public override void InstallBindings()
    {
        this.Container
            .BindInterfacesTo<Server>()
            .AsCached()
            .WithArguments(url, port);
    }
}