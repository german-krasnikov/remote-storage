using System.Collections.Generic;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Damage : MonoBehaviour, ISerializedComponent
    {
        private const string ValueKey = "Value";
        
        ///Const
        [field: SerializeField]
        public int Value { get; private set; } = 10;
        public void Save(Dictionary<string, string> data) => data[ValueKey] = Value.ToString();

        public void Load(Dictionary<string, string> data) => Value = int.Parse(data[ValueKey]);
    }
}