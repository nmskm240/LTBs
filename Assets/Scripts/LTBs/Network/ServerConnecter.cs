using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using LTBs.System;

namespace LTBs.Network
{
    public class ServerConnecter : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Image Back;
        [SerializeField]
        private GameObject ConnectingText;
        [SerializeField]
        private GameObject ClickToConnectServerText;

        public void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectingShow();
        }

        private void ConnectingShow()
        {
            Back.color = new Color(0.59f, 0.59f, 0.59f, 1f);
            ConnectingText.SetActive(true);
            ClickToConnectServerText.SetActive(false);
        }

        public override void OnConnectedToMaster() 
        {
            CustomTypesRegister.Register();
            SceneManager.LoadScene("LobbyScene");
        }
    }
}