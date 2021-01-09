using Photon.Realtime;

namespace LTBs.System
{
    public interface IRoundCallbacks
    {
        void OnRoundBegins(int round, Player nextStartPlayer);
        void OnRoundCompleted(int round, Player nextStartPlayer);
    }
}