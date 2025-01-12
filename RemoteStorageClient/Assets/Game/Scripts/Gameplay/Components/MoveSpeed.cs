using System.Collections.Generic;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class MoveSpeed : MonoBehaviour, ISerializedComponent
    {
        private const string CurrentKey = "Current";
        ///Const
        [field: SerializeField]
        public float Current { get; private set; }
        public void Save(Dictionary<string, string> data)
        {
            data[CurrentKey] = Current.ToString();
        }

        public void Load(Dictionary<string, string> data)
        { 
            Current = float.Parse(data[CurrentKey]);
        }
    }
}