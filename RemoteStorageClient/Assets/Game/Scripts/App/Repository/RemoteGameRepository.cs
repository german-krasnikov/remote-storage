using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SampleGame.App
{
    public class RemoteGameRepository
    {
        private string ServerUrl = "http://127.0.0.1:8888/";

        public async UniTask<string> LoadState(int version)
        {
            using UnityWebRequest request = UnityWebRequest.Get($"{ServerUrl}load?version={version}");
            await request.SendWebRequest().ToUniTask();

            if (request.result != UnityWebRequest.Result.Success) return null;
            return request.downloadHandler.text;
        }

        public async UniTask<bool> SaveState(int version, string data)
        {
            using UnityWebRequest request = new UnityWebRequest($"{ServerUrl}save?version={version}", "PUT");
            var bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            await request.SendWebRequest().ToUniTask();
            return request.result == UnityWebRequest.Result.Success;
        }
    }
}