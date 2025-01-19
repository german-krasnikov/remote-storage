using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    public sealed class GameSaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly EntityWorld _entityWorld;
        private readonly DiContainer _container;
        private readonly RemoteGameRepository _remoteGameRepository;

        public GameSaveLoader(
            DiContainer container,
            IGameRepository repository,
            RemoteGameRepository remoteGameRepository,
            EntityWorld entityWorld)
        {
            _remoteGameRepository = remoteGameRepository;
            _container = container;
            _repository = repository;
            _entityWorld = entityWorld;
        }

        public async UniTask<bool> Save(int version)
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

                entity.gameObject.SaveTransform(entityState);
                gameState.Add(GetEntityState(entity), JsonConvert.SerializeObject(entityState));
            }

            _repository.SetState(version, gameState);
            return await _remoteGameRepository.SaveState(version, _repository.EncryptToString(gameState));
        }

        public async UniTask<bool> Load(int version)
        {
            _entityWorld.DestroyAll();
            var gameState = _repository.GetState(out var localVersion);
            var remoteGameState = await _remoteGameRepository.LoadState(version);

            if (remoteGameState != null && localVersion <= version)
            {
                gameState = _repository.DecryptToString(remoteGameState);
            }

            foreach (var pair in gameState)
            {
                CreateEntity(pair.Key);
            }

            foreach (var pair in gameState)
            {
                var entityId = GetEntityId(pair.Key);
                var entity = _entityWorld.Get(entityId);
                _container.InjectGameObject(entity.gameObject);
                var entityState = JsonConvert.DeserializeObject<Dictionary<string, object>>(pair.Value);
                entity.gameObject.LoadTransform(entityState);

                foreach (var component in entity.GetComponents<ISerializedComponent>())
                {
                    if (entityState.TryGetValue(component.GetType().Name, out var json))
                    {
                        var state = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)json);
                        component.Load(state);
                    }
                }
            }

            return true;
        }

        private string GetEntityState(Entity entity) => $"{entity.Id}_{entity.Name}_{entity.Type}";

        private Entity CreateEntity(string state)
        {
            var parts = state.Split(new[] { "_" }, StringSplitOptions.None);
            var id = int.Parse(parts[0]);
            var name = parts[1];
            return _entityWorld.Spawn(name, Vector3.zero, Quaternion.identity, id);
        }

        private int GetEntityId(string state) => int.Parse(state.Split(new[] { "_" }, StringSplitOptions.None)[0]);
    }
}