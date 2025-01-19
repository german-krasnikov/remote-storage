using System.Collections.Generic;
using Modules.Ecryption;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class GameRepository : IGameRepository
    {
        private readonly string VersionKey = "Version";
        private readonly string PrefValue = "GameRepository";
        private readonly string _aesPassword;
        private readonly byte[] _aesSalt;

        public GameRepository(string aesPassword, byte[] aesSalt)
        {
            _aesPassword = aesPassword;
            _aesSalt = aesSalt;
        }

        public Dictionary<string, string> GetState(out int version)
        {
            version = 0;
            if (!int.TryParse(PlayerPrefs.GetString(VersionKey), out version)) return null;
            return DecryptToString(PlayerPrefs.GetString(PrefValue));
        }

        public void SetState(int version, Dictionary<string, string> gameState)
        {
            PlayerPrefs.SetString(VersionKey, version.ToString());
            PlayerPrefs.SetString(PrefValue, EncryptToString(gameState));
        }

        public string EncryptToString(Dictionary<string, string> gameState)
        {
            var json = JsonConvert.SerializeObject(gameState);
            return AesEncryptor.Encrypt(json, _aesPassword, _aesSalt);
        }

        public Dictionary<string, string> DecryptToString(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new Dictionary<string, string>();

            string json = AesEncryptor.Decrypt(data, _aesPassword, _aesSalt);
            Debug.Log($"Loaded state: {json}");

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (result == null)
                return new Dictionary<string, string>();

            return result;
        }
    }
}