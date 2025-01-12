using Zenject;

DiContainer diContainer = new DiContainer();
diContainer.Install<ProgramInstaller>();
diContainer.ResolveRoots();

InitializableManager initializableManager = diContainer.Resolve<InitializableManager>();
initializableManager.Initialize();

Console.WriteLine("Start Programm...");

while (true)
    if (Console.ReadKey() is {Key: ConsoleKey.Q})
        break;

diContainer.Resolve<DisposableManager>().Dispose();
Console.WriteLine("Quit Programm...");