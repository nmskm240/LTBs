using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.System;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.UI
{    
    public class TurnMoveSelecter : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private GameObject BuildButton;
        [SerializeField]
        private GameObject WorkButton;
        [SerializeField]
        private GameObject CancelButton;

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("PMove") && targetPlayer == PhotonNetwork.LocalPlayer)
            {
                PlayerMoveType playerMove = targetPlayer.GetPlayerMove();
                switch(playerMove)
                {
                    case PlayerMoveType.Build:
                    case PlayerMoveType.Selecting:
                        BuildButton.SetActive(false);
                        WorkButton.SetActive(false);
                        CancelButton.SetActive(false);
                        break;
                    case PlayerMoveType.Work:
                    case PlayerMoveType.BuildPosSelecting:
                        BuildButton.SetActive(false);
                        WorkButton.SetActive(false);
                        CancelButton.SetActive(true);
                        break;
                    case PlayerMoveType.None:
                        BuildButton.SetActive(true);
                        WorkButton.SetActive(true);
                        CancelButton.SetActive(false);
                        break;
                    case PlayerMoveType.NonTurn:
                        BuildButton.SetActive(false);
                        WorkButton.SetActive(false);
                        CancelButton.SetActive(false);
                        GameObject.Find("TurnManager").GetComponent<TurnAndRoundManager>().TurnEnd();
                        break;
                }
            }
        } 
    }
}