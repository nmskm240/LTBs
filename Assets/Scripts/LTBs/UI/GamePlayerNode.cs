using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.System;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.UI
{    
    public class GamePlayerNode : MonoBehaviourPunCallbacks, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Player inChargePlayer;
        [SerializeField]
        private Image playerImage;
        [SerializeField]
        private Text playerName;
        [SerializeField]
        private ResourceCounter wood;
        [SerializeField]
        private ResourceCounter stone;
        [SerializeField]
        private ResourceCounter fish;
        [SerializeField]
        private ResourceCounter wheat;
        [SerializeField]
        private ResourceCounter money;
        [SerializeField]
        private ResourceCounter point;

        private Dictionary<ResourceType, int> resource;
        private Animation Animation;

        public Player InChargePlayer 
        {
            set
            {
                inChargePlayer = value;
                playerName.text = inChargePlayer.NickName; 
                playerImage.color = inChargePlayer.GetColor();
                this.gameObject.name = inChargePlayer.ActorNumber.ToString();
            }
        }
        public Dictionary<ResourceType, int> PlayerResource
        {
            get
            {
                resource = new Dictionary<ResourceType, int>()
                {
                    {ResourceType.Stone, stone.Count},
                    {ResourceType.Wood, wood.Count},
                    {ResourceType.Fish, fish.Count},
                    {ResourceType.Wheat, wheat.Count},
                    {ResourceType.Money, money.Count},
                    {ResourceType.Point, point.Count},
                };
                return resource;
            }
        }

        private void OnTransformParentChanged() 
        {
            Animation = this.transform.parent.gameObject.GetComponent<Animation>();    
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(targetPlayer == inChargePlayer)
            {
                if(propertiesThatChanged.ContainsKey("Resource"))
                {
                    Dictionary<ResourceType, int> resources = targetPlayer.GetResource();
                    foreach(KeyValuePair<ResourceType, int> resource in resources)
                    {
                        ResourceCounter resourceCounter = null;
                        switch(resource.Key)
                        {
                            case ResourceType.Wood:
                                resourceCounter = wood;
                                break;
                        case ResourceType.Stone:
                                resourceCounter = stone;
                                break;
                            case ResourceType.Fish:
                                resourceCounter = fish;
                                break;
                            case ResourceType.Wheat:
                                resourceCounter = wheat;
                                break;
                            case ResourceType.Money:
                                resourceCounter = money;
                                break;
                            case ResourceType.Point:
                                resourceCounter = point;
                                break;
                        }
                        resourceCounter.Set(resource.Value);
                    }
                }
            }
        }

        public void OnPointerEnter(PointerEventData e)
        {
            if(!Animation.IsPlaying("ShowGamePlayerNodes"))
            {
                Animation["ShowGamePlayerNodes"].speed = 1f;
                Animation.Play();
            }
            // Debug.Log("pointer enter");
        }

        public void OnPointerExit(PointerEventData e)
        {
            Animation["ShowGamePlayerNodes"].speed = -1f;
            Animation.Play();
        }
    }
}