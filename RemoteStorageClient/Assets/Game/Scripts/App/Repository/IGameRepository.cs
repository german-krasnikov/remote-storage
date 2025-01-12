using System.Collections.Generic;

namespace SampleGame.App
{
    public interface IGameRepository
    {
        Dictionary<string, string> GetState();
        void SetState(Dictionary<string, string> gameState);
    }
}