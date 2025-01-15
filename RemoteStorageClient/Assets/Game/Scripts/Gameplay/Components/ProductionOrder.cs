using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISerializedComponent
    {
        private const string QueueKey = "Queue";

        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;

        private EntityCatalog _entityCatalog;

        [Inject]
        private void Construct(EntityCatalog entityCatalog)
        {
            _entityCatalog = entityCatalog;
        }

        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
        public void Save(Dictionary<string, string> data) => data[QueueKey] = string.Join(",", _queue.Select(config => config.Name));

        public void Load(Dictionary<string, string> data)
        {
            var ids = data[QueueKey].Split(new[] { "," }, StringSplitOptions.None);
            _queue.Clear();

            foreach (var id in ids)
            {
                if (_entityCatalog.FindConfig(id, out EntityConfig config))
                    _queue.Add(config);
            }
        }
    }
}