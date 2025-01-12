using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour, ISerializedComponent
    {
        private const string CurrentKey = "Current";
        private const string DurationKey = "Duration";
        
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField]
        public float Duration { get; private set; }
        public void Save(Dictionary<string, string> data)
        {
            data[CurrentKey] = Current.ToString();
            data[DurationKey] = Duration.ToString();
        }

        public void Load(Dictionary<string, string> data)
        {
            Current = float.Parse(data[CurrentKey]);
            Duration = float.Parse(data[DurationKey]);
        }
    }
}