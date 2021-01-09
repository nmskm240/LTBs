using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Game.UI;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class BuildableListViewer : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private GameObject Contents;

        private List<BuildingNode> Nodes = new List<BuildingNode>();

        private void Awake() 
        {
            GetComponent<Canvas>().enabled = false;    
            SetBuildList();
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

        private void SetBuildList()
        {
            var tileFactroy = new TileFactory();
            var buildingNodeFactory = new BuildingNodeFactory();
            var buildings = PhotonNetwork.CurrentRoom.GetBuild();
            foreach(int buildId in buildings)
            {
                var tileObj = tileFactroy.Create(buildId.ToString());
                var buildingNodeObj = buildingNodeFactory.Create();
                var tile = tileObj.GetComponent<Tile>();
                var buildingNode = buildingNodeObj.GetComponent<BuildingNode>();
                var tmp = buildingNode.OwnerNameObj.transform.GetChild(0).gameObject;
                Nodes.Add(buildingNode);
                buildingNode.Tile = tile;
                buildingNode.OwnerName = "勝利点:" + tile.Points + "\nコスト\n";
                foreach(var cost in tile.BuildCost)
                {
                    var go = Instantiate(tmp);
                    go.GetComponent<Image>().sprite = Resources.Load("Textures/" + Enum.GetName(typeof(ResourceType), cost), typeof(Sprite)) as Sprite;
                    go.transform.SetParent(buildingNode.OwnerNameObj.transform); 
                    go.SetActive(true);
                }
                buildingNode.ExecuteButton.onClick.AddListener(() => Builder.BuildSetup(tile));
                buildingNodeObj.transform.SetParent(Contents.transform);
                Destroy(tileObj);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("PMove") && targetPlayer == PhotonNetwork.LocalPlayer)
            {
                var playerMoveType = targetPlayer.GetPlayerMove();
                if(playerMoveType == PlayerMoveType.Build)
                {
                    GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    GetComponent<Canvas>().enabled = false;
                }
            }
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("BuildU"))
            {
                var buildingId = PhotonNetwork.CurrentRoom.GetBuildUpdate();
                foreach(Transform tf in Contents.transform)
                {
                    if(tf.gameObject.activeSelf)
                    {
                        if(tf.gameObject.GetComponent<BuildingNode>().TileId == buildingId)
                        {
                            Nodes.Remove(tf.gameObject.GetComponent<BuildingNode>());
                            tf.gameObject.SetActive(false);
                            break;
                        }
                    }
                }
            }
        }
    }
}