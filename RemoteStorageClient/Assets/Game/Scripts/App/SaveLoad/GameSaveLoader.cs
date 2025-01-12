using System;
using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

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
                var entityState = new Dictionary<string, object>();
                var components = entity.GetComponents<ISerializedComponent>();

                foreach (var component in components)
                {
                    var componentState = new Dictionary<string, string>();
                    component.Save(componentState);
                    entityState.Add(component.GetType().Name, JsonConvert.SerializeObject(componentState));
                }

                gameState.Add(GetEntityState(entity), JsonConvert.SerializeObject(entityState));
            }

            _repository.SetState(gameState);
        }

        public void Load()
        {
            _entityWorld.DestroyAll();
            Dictionary<string, string> gameState = _repository.GetState();

            foreach (var pair in gameState)
            {
                CreateEntity(pair.Key);
            }

            foreach (var pair in gameState)
            {
                var entityId = GetEntityId(pair.Key);
                var entity = _entityWorld.Get(entityId);
                var entityState = JsonConvert.DeserializeObject<Dictionary<string, object>>(pair.Value);

                foreach (var component in entity.GetComponents<ISerializedComponent>())
                {
                    if (entityState.TryGetValue(component.GetType().Name, out var json))
                    {
                        var state = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)json);
                        component.Load(state);
                    }
                }
            }
        }

        private string GetEntityState(Entity entity) => $"{entity.Id}_{entity.Name}_{entity.Type}";

        private Entity CreateEntity(string state)
        {
            var parts = state.Split(new[] { "_" }, StringSplitOptions.None);
            var id = int.Parse(parts[0]);
            var name = parts[1];
            //var type = (EntityType)int.Parse(parts[2]);
            return _entityWorld.Spawn(name, Vector3.zero, Quaternion.identity, id);
        }

        private int GetEntityId(string state) => int.Parse(state.Split(new[] { "_" }, StringSplitOptions.None)[0]);
    }
}