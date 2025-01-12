using Zenject;

public sealed class ProgramInstaller : Installer
{
    public override void InstallBindings()
    {
        this.Container.Bind<InitializableManager>().AsSingle();
        this.Container.Bind<DisposableManager>().AsSingle();
        
        this.Container.Install<ServerInstaller>();
        this.Container.Install<MiddlewareInstaller>();
        this.Container.Install<RepositoryInstaller>();
    }
}