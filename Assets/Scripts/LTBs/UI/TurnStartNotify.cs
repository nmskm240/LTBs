using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.UI
{    
    public class TurnStartNotify : MonoBehaviour 
    {
        [SerializeField]
        private Text NotifyText;
        [SerializeField]
        private Animation Animation;

        private string YourTurn = "Your turn";
        private string OtherTurn = "'s turn";
        private Player inChargePlayer;
        
        public Player InChargePlayer 
        {
            set
            {
                NotifyText.text = (value == PhotonNetwork.LocalPlayer) ? YourTurn : value.NickName + OtherTurn;
            } 
        } 

        private IEnumerator ShowProcess()
        {
            var canvas = GetComponent<Canvas>();
            canvas.enabled = true;
            Animation.Play();
            yield return new WaitWhile(() => { return Animation.IsPlaying("TurnStart"); });
            canvas.enabled = false;
        }

        private void Awake() 
        {
            GetComponent<Canvas>().enabled = false;
        }

        public void Show()
        {
            StartCoroutine(ShowProcess());
        }
    }
}