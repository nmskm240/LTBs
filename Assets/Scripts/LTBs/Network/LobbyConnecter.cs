using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.Network
{    
    public class LobbyConnecter : MonoBehaviourPunCallbacks 
    {
        private void Awake()
        {
            if(PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnJoinedLobby()
        {
            PhotonNetwork.LocalPlayer.NickName = "Guest" + Random.Range(1000,10000);
        }
    }
}