using System.Collections.Generic;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ResourceBag : MonoBehaviour, ISerializedComponent
    {
        private string CurrentKey => $"{nameof(Current)}";
        private string TypeKey => $"{nameof(Type)}";
        private string CapacityKey => $"{nameof(Capacity)}";
        
        ///Variable
        [field: SerializeField]
        public ResourceType Type { get; set; }
        
        ///Variable
        [field: SerializeField]
        public int Current { get; set; }
        
        ///Const
        [field: SerializeField]
        public int Capacity { get; set; }
        public void Save(Dictionary<string, string> data)
        {
            data[CurrentKey] = Current.ToString();
            data[TypeKey] = Type.ToString();
            data[CapacityKey] = Capacity.ToString();
        }

        public void Load(Dictionary<string, string> data)
        {
            Current = int.Parse(data[CurrentKey]);
            Type = (ResourceType)int.Parse(data[TypeKey]);
            Capacity = int.Parse(data[CapacityKey]);
        }
    }
}