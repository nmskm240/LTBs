using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.UI
{    
    public class FoodDistributionSelecter : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private GameObject Contents;

        private List<FoodDistributionNode> Nodes = new List<FoodDistributionNode>();

        private void Awake() 
        {
            GetComponent<Canvas>().enabled = false;
        }

        private void Update() 
        {
            if(GetComponent<Canvas>().enabled)
            {
                foreach(var node in Nodes)
                {
                    node.ManagedUpdate();
                }
            }    
        }

        public void Show()
        {
            foreach(var node in Nodes)
            {
                node.Reset();
            }
            GetComponent<Canvas>().enabled = true;
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("MaxWorker"))
            {
                var factory = new FoodDistributionNodeFactory();
                for(var i = 0; i < PhotonNetwork.CurrentRoom.GetMaxWorker(); i++)
                {
                    var go = factory.Create();
                    Nodes.Add(go.GetComponent<FoodDistributionNode>());
                    go.transform.SetParent(Contents.transform);
                }
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("PMove") && targetPlayer == PhotonNetwork.LocalPlayer && targetPlayer.GetPlayerMove() == PlayerMoveType.RoundComplet)
            {
                GetComponent<Canvas>().enabled = false;
            }
        }
    }
}