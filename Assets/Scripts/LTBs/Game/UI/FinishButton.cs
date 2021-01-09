using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class FinishButton : MonoBehaviour 
    {
        public void OnClick()
        {
            PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.NonTurn);
            this.transform.root.gameObject.GetComponent<Canvas>().enabled = false;
        }       
    }
}