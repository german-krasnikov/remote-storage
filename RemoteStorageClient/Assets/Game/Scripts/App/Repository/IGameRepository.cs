using System.Collections.Generic;

namespace SampleGame.App
{
    public interface IGameRepository
    {
        Dictionary<string, string> GetState(out int version);
        int SetState(Dictionary<string, string> gameState);
        string EncryptToString(Dictionary<string, string> gameState);
        Dictionary<string, string> DecryptToString(string data);
    }
}