using System;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class StorageUtils
    {
        public static string Vector3ToString(this Vector3 v) => $"{v.x:0.000},{v.y:0.000},{v.z:0.000}";

        public static Vector3 StringToVector3(this string s)
        {
            var parts = s.Split(new[] { "," }, StringSplitOptions.None);
            return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
        }
    }
}