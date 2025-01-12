using System.Collections.Generic;
using Game.Scripts.Utils;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour, ISerializedComponent
    {
        private const string ValueKey = "Value";
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }

        public void Save(Dictionary<string, string> data) => data[ValueKey] = Value.Vector3ToString();

        public void Load(Dictionary<string, string> data) => Value = data[ValueKey].Vector3FromString();
    }
}