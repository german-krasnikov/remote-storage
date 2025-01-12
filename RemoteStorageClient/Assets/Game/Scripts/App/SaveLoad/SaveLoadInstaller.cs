using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "SaveLoadInstaller",
        menuName = "Zenject/App/New SaveLoadInstaller"
    )]
    public sealed class SaveLoadInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private float _savePeriod = 5;
        
        public override void InstallBindings()
        {
            this.Container.Bind<GameSaveLoader>().AsSingle();
        }
    }
}