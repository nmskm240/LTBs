using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.RaiseEvents;

namespace LTBs.Network
{    
    public class GameConnecter : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private Button GameStartButton;

        public void Connect()
        {
            if(CanConnect())
            {
                // if(PhotonNetwork.IsMasterClient)
                // {
                //     PhotonNetwork.CurrentRoom.IsOpen = false;
                // }
                PhotonNetwork.RaiseEvent((byte)RaiseEventType.LoadGameScene, "", new RaiseEventOptions(){ Receivers = ReceiverGroup.All, }, new SendOptions());
                // SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.LogError("Not enough players");
            }
        }

        private bool CanConnect()
        {
            bool tmp = false;
            
            if(PhotonNetwork.PlayerList.Length >= 2 && PhotonNetwork.IsMasterClient)
            {
                tmp = true;
            }
            return tmp;
        }

        private void Awake() 
        {
            GameStartButton.interactable = CanConnect();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)	
        {
            GameStartButton.interactable = CanConnect();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            GameStartButton.interactable = CanConnect();
        }
    }
}