using System;
using System.Collections.Generic;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour, ISerializedComponent
    {
        private const string TypeKey = "Type";
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }

        public void Save(Dictionary<string, string> data)
        {
            data[TypeKey] = Type.ToString();
        }

        public void Load(Dictionary<string, string> data)
        {
            Enum.TryParse(data[TypeKey], out TeamType Type);
        }
    }
}