using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



namespace LTBs.Network
{    
    public class RoomExiter : MonoBehaviourPunCallbacks 
    {
        public void Exit()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}