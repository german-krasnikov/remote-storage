using Zenject;

public sealed class MiddlewareInstaller : Installer
{
    public override void InstallBindings()
    {
        this.Container
            .Bind<IMiddleware>()
            .FromMethod(this.CreateMiddlewareTree)
            .AsSingle();
    }

    private IMiddleware CreateMiddlewareTree(InjectContext ctx)
    {
        DiContainer container = ctx.Container;
        return new MiddlewareSelector(
            container.Instantiate<LoadMiddleware>(),
            container.Instantiate<SaveMiddleware>(),
            container.Instantiate<NotFoundMiddleware>()
        );
    }
}