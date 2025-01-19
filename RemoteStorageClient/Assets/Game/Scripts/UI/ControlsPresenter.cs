using System;
using Cysharp.Threading.Tasks;
using SampleGame.App;
using Zenject;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private GameSaveLoader _gameSaveLoader;

        [Inject]
        private void Construct(GameSaveLoader gameSaveLoader) => _gameSaveLoader = gameSaveLoader;

        public void Save(Action<bool, int> callback) => SaveAsync(callback).Forget();

        public void Load(string versionText, Action<bool, int> callback) => LoadAsync(versionText, callback).Forget();

        private async UniTask SaveAsync(Action<bool, int> callback)
        {
            var result = await _gameSaveLoader.Save();
            callback.Invoke(result.Item1, result.Item2);
        }

        private async UniTask LoadAsync(string versionText, Action<bool, int> callback)
        {
            if (!int.TryParse(versionText, out var version))
            {
                callback.Invoke(false, -1);
                return;
            }

            var result = await _gameSaveLoader.Load(version);
            callback.Invoke(result, version);
        }
    }
}