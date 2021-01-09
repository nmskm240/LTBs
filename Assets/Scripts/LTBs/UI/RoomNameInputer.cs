using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network;

namespace LTBs.UI
{    
    public class RoomNameInputer : MonoBehaviour 
    {
        [SerializeField]
        private Text RoomNameText;

        private void Awake() 
        {
            Hide();
        }

        public void Hide()
        {
            GetComponent<Canvas>().enabled = false;
        }

        public void Show()
        {
            GetComponent<Canvas>().enabled = true;
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(RoomNameText.text, new RoomOptions(){ MaxPlayers = 4 }, null);
        }
    }
}