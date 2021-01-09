using UnityEngine;
using Photon.Pun;

namespace LTBs.Network
{
    public class RoomEntry : MonoBehaviour
    {
        public void Entry(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}