using System.Collections.Generic;

namespace SampleGame.Gameplay
{
    public interface ISerializedComponent
    {
        void Save(Dictionary<string, string> data);
        void Load(Dictionary<string, string> data);
    }
}