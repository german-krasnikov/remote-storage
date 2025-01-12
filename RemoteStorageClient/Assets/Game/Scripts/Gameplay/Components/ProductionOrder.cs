using System.Collections.Generic;
using Modules.Entities;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;
        
        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
        public void Save(Dictionary<string, string> data)
        {
            
        }

        public void Load(Dictionary<string, string> data)
        {
            
        }
    }
}