using System;
using System.Collections.Generic;
using Game.Scripts.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
    public static class TransformStorageHelper
    {
        private const string TransformKey = "Transform";
        private const string PositionKey = "Position";
        private const string RotationKey = "Rotation";

        public static void SaveTransform(this GameObject component, Dictionary<string, object> entityState)
        {
            var data = new Dictionary<string, string>
            {
                [PositionKey] = component.transform.position.Vector3ToString(),
                [RotationKey] = component.transform.rotation.eulerAngles.Vector3ToString(),
            };
            entityState.Add(TransformKey, JsonConvert.SerializeObject(data));
        }

        public static void LoadTransform(this GameObject component, Dictionary<string, object> entityState)
        {
            if (!entityState.TryGetValue(TransformKey, out var json)) return;
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)json);
            if (data == null) return;
            component.transform.position = data[PositionKey].StringToVector3();
            component.transform.rotation = Quaternion.Euler(data[RotationKey].StringToVector3());
        }
    }
}