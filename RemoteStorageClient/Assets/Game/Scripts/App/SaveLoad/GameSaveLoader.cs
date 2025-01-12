using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace SampleGame.App
{
    public sealed class GameSaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly EntityWorld _entityWorld;

        public GameSaveLoader(IGameRepository repository, EntityWorld entityWorld)
        {
            _repository = repository;
            _entityWorld = entityWorld;
        }

        public void Save()
        {
            var gameState = new Dictionary<string, string>();
            var entities = _entityWorld.GetAll();

            foreach (Entity entity in entities)
            {
                var entityState = new Dictionary<string, string>();
                var components = entity.GetComponents<ISerializedComponent>();

                foreach (var component in components)
                {
                    var componentState = new Dictionary<string, string>();
                    component.Save(componentState);
                    entityState.Add(component.GetType().Name, JsonConvert.SerializeObject(componentState));
                }
                gameState.Add(entity.Id.ToString(), JsonConvert.SerializeObject(entityState));
            }
            
            //foreach (IGameSerializer serializer in _serializers)
            //    serializer.Serialize(gameState);

            _repository.SetState(gameState);
        }

        public void Load()
        {
            Dictionary<string, string> gameState = _repository.GetState();
            //foreach (IGameSerializer serializer in _serializers)
            //    serializer.Deserialize(gameState);
        }
    }
}