using System.Collections.Generic;
using System.IO;
using System.Text;
using Modules.Ecryption;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class GameRepository : IGameRepository
    {
        private readonly string PrefValue = "GameRepository";
        private readonly string _aesPassword;
        private readonly byte[] _aesSalt;

        public GameRepository(string aesPassword, byte[] aesSalt)
        {
            _aesPassword = aesPassword;
            _aesSalt = aesSalt;
        }

        public Dictionary<string, string> GetState()
        {
            var data = PlayerPrefs.GetString(PrefValue);
            if (string.IsNullOrEmpty(data))
                return new Dictionary<string, string>();

            string json = AesEncryptor.Decrypt(data, _aesPassword, _aesSalt);
            Debug.Log($"Loaded state: {json}");

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (result == null)
                return new Dictionary<string, string>();

            return result;
        }

        public void SetState(Dictionary<string, string> gameState)
        {
            var json = JsonConvert.SerializeObject(gameState);
            var encryptedBytes = AesEncryptor.Encrypt(json, _aesPassword, _aesSalt);
            PlayerPrefs.SetString(PrefValue, encryptedBytes);
            Debug.Log($"Save state: {json}");
        }
    }
}