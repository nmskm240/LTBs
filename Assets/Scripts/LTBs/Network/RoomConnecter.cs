using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using LTBs.UI;
using LTBs.Network.CustomProperties.Players;

namespace GameConnecter
{
    public class RoomConnecter : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LocalPlayer.SetColor(new ColorFactory().Create());
            SceneManager.LoadScene("RoomScene");
        }
    }
}