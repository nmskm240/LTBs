using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.System;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.Game.UI
{    
    public class BuildingAbilityListViewer : MonoBehaviour
    {
        [SerializeField]
        private GameObject Contents;

        private List<BuildingNode> Nodes = new List<BuildingNode>();

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

        public void Show(IEnumerable<Tile> tiles) 
        {
            if(Contents.transform.childCount != 0)
            {
                foreach(Transform tf in Contents.transform)
                {
                    tf.gameObject.SetActive(false);
                }
                Nodes.Clear();
            }
            if(tiles.Count() <= 0)
            {
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.NonTurn);
                GetComponent<Canvas>().enabled = false;
                return;
            }
            var BuildingNodeFactory = new BuildingNodeFactory();
            foreach(Tile tile in tiles)
            {
                var go = BuildingNodeFactory.Create();
                var node = go.GetComponent<BuildingNode>();
                Nodes.Add(node);
                node.Tile = tile;
                node.OwnerName = (tile.Owner == null) ? "" : tile.Owner.NickName; 
                node.ExecuteButton.onClick.AddListener(() => 
                {
                    if(tile.Owner != null &&  tile.Owner != PhotonNetwork.LocalPlayer)
                    {
                        Resource.Send(ResourceType.Money, PhotonNetwork.LocalPlayer, tile.Owner);
                    }
                    tile.NormalAbility.Invoke();
                    node.AbilityActivate();
                });
                go.transform.SetParent(Contents.transform);
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.Selecting);
                GetComponent<Canvas>().enabled = true;
            }
        }
    }
}