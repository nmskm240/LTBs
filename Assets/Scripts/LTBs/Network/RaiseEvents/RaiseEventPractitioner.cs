using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.Network.RaiseEvents
{    
    public abstract class RaiseEventPractitioner : MonoBehaviour, IOnEventCallback
    {
        public virtual void OnEvent(EventData photonEvent)
        {

        }

        public void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}