using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.System;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.UI
{    
    public class GamePlayerListViewer : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject Contents;

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("TurnCycle"))
            {
                var GamePlayerFactory = new GamePlayerFactory();
                foreach(Player player in PhotonNetwork.CurrentRoom.GetTurnCycle())
                {
                    var go = GamePlayerFactory.Create();
                    go.transform.SetParent(Contents.transform);
                    var gamePlayerNode = go.GetComponent<GamePlayerNode>();
                    gamePlayerNode.InChargePlayer = player;
                }
            }
        }
    }
}