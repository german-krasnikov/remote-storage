using System.Collections.Generic;

namespace SampleGame.App
{
    public interface IGameSerializer
    {
        void Serialize(IDictionary<string, string> saveState);
        void Deserialize(IDictionary<string, string> loadState);
    }
}