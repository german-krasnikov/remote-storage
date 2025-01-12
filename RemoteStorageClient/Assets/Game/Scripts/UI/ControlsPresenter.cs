using System;
using SampleGame.App;
using Zenject;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private GameSaveLoader _gameSaveLoader;
        
        [Inject]
        private void Construct(GameSaveLoader gameSaveLoader) => _gameSaveLoader= gameSaveLoader;

        public void Save(Action<bool, int> callback)
        {
            _gameSaveLoader.Save();
            callback.Invoke(false, -1);
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            _gameSaveLoader.Load();
            callback.Invoke(false, -1);
        }
    }
}