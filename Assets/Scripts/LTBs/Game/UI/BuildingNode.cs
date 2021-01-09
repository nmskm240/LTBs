using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class BuildingNode : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private Text buildingName;
        [SerializeField]
        private Text buildingAbilityInfo;
        [SerializeField]
        private Text ownerName;
        [SerializeField]
        private GameObject ownerNameObj;
        [SerializeField]
        private Button executeButton;

        private Tile tile;

        public Tile Tile 
        {
            set 
            {
                tile = value; 
                buildingName.text = tile.Name;
                buildingAbilityInfo.text = tile.AbilityText;
            }
        }
        public int TileId { get { return tile.Id; } }
        public string OwnerName { set { ownerName.text = value; } }
        public GameObject OwnerNameObj { get { return ownerNameObj; } }
        public Button ExecuteButton { get { return executeButton; } }
        public bool Executed { get; private set; } = false;

        public void ManagedUpdate()
        {
            switch(PhotonNetwork.LocalPlayer.GetPlayerMove())
            {
                case PlayerMoveType.Build:
                    executeButton.interactable = Builder.CanBuild(this.tile);
                    break;
                case PlayerMoveType.Selecting:
                    var canActivate = true;
                    var playerResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
                    var abilityCost = new Dictionary<ResourceType, int>();
                    foreach(var rt in tile.AbilityCost)
                    {
                        if(abilityCost.ContainsKey(rt))
                        {
                            abilityCost[rt]++;
                        }
                        else
                        {
                            abilityCost.Add(rt, 1);
                        }
                    }

                    if(tile.Owner != PhotonNetwork.LocalPlayer && tile.Owner != null)
                    {
                        if(abilityCost.ContainsKey(ResourceType.Money))
                        {
                            abilityCost[ResourceType.Money]++;
                        }
                        else
                        {
                            abilityCost.Add(ResourceType.Money, 1);
                        }
                    }

                    if(abilityCost.ContainsKey(ResourceType.All))
                    {
                        var tmp = 0;
                        foreach(var resource in playerResource)
                        {
                            if(resource.Key == ResourceType.Money || resource.Key == ResourceType.Point)
                            {
                                continue;
                            }
                            tmp += resource.Value;
                        }
                        canActivate = (tmp >= abilityCost[ResourceType.All]) ? true : false;
                    }
                    else
                    {
                        foreach(var cost in abilityCost)
                        {
                            if(cost.Value > playerResource[cost.Key])
                            {
                                canActivate = false;
                                break;
                            }
                        }
                    }
                    executeButton.interactable = (Executed) ? false : canActivate;
                    break;
            }
        }

        public void AbilityActivate()
        {
            // GetComponent<Image>().color = new Color(0,0,0);
            var factory = new CompletedMarkFactory();
            var go = factory.Create();
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector2(-25, 0);
            executeButton.interactable = false;
            Executed = true;
        }
    }
}