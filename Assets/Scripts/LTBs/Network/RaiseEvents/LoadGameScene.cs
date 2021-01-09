using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;


namespace LTBs.Network.RaiseEvents
{
    public class LoadGameScene : RaiseEventPractitioner
    {
        public override void OnEvent(EventData photonEvent)
        {
            if(photonEvent.Code == (byte)RaiseEventType.LoadGameScene)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}