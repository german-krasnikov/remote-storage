using System;
using System.Collections.Generic;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Health : MonoBehaviour, ISerializedComponent
    {
        private string CurrentKey => nameof(Current);
        private string MaxKey => nameof(Max);
        
        ///Variable
        [field: SerializeField]
        public int Current { get; set; } = 50;

        ///Const
        [field: SerializeField]
        public int Max { get; private set; } = 100;
        public void Save(Dictionary<string, string> data)
        {
            Current = Convert.ToInt32(data[CurrentKey]);
            Max = Convert.ToInt32(data[MaxKey]);
        }

        public void Load(Dictionary<string, string> data)
        {
            data[CurrentKey] = Current.ToString();
            data[MaxKey] = Max.ToString();
        }
    }
}