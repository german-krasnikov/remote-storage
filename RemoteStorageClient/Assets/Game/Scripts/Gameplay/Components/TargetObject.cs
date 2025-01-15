using System.Collections.Generic;
using Modules.Entities;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISerializedComponent
    {
        private const string ValueIdKey = "ValueId";
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }

        private EntityWorld _entityWorld;

        [Inject]
        private void Construct(EntityWorld entityWorld)
        {
            _entityWorld = entityWorld;
        }

        public void Save(Dictionary<string, string> data)
        {
            if (Value == null) return;
            data[ValueIdKey] = Value.Id.ToString();
        }

        public void Load(Dictionary<string, string> data)
        {
            Value = null;
            if (!data.TryGetValue(ValueIdKey, out var value)) return;
            var valueId = int.Parse(value);
            Value = _entityWorld.Get(valueId);
        }
    }
}