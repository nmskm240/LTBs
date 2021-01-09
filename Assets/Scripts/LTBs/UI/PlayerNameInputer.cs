using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network;

namespace LTBs.UI
{    
    public class PlayerNameInputer : MonoBehaviour 
    {
        [SerializeField]
        private Text PlayerNameText;

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
            PlayerNameText.text = PhotonNetwork.LocalPlayer.NickName;
        }

        public void SetName()
        {
            PhotonNetwork.LocalPlayer.NickName = (string.IsNullOrWhiteSpace(PlayerNameText.text)) ? "Guest" + Random.Range(1000, 10000).ToString() : PlayerNameText.text;
            Hide();
        }
    }
}