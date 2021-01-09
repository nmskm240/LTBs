using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.Game
{    
    public class MidwayExitDetecter : MonoBehaviourPunCallbacks 
    {
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}