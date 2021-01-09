using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.System;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.UI
{    
    public class BuildButton : MonoBehaviour 
    {
        public void OnClick()
        {
            PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.Build);
        }
    }
}