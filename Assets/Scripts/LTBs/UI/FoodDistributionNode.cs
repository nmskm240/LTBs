using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.UI
{    
    public class FoodDistributionNode : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Image WorkerImage;
        [SerializeField]
        private Button FishButton;
        [SerializeField]
        private Button WheatButton;
        
        private GameObject go = null;

        public bool IsDistributed { get; private set; } = false;

        private void Awake() 
        {
            FishButton.onClick.AddListener(() => 
            {
                var factory = new CompletedMarkFactory();
                go = factory.Create();
                go.transform.SetParent(this.transform);
                go.transform.localPosition = new Vector2(-25, 0);
                Distribution(ResourceType.Fish);
                FishButton.interactable = false;
                WheatButton.interactable = false;
                IsDistributed = true;
            });
            WheatButton.onClick.AddListener(() => 
            {
                var factory = new CompletedMarkFactory();
                go = factory.Create();
                go.transform.SetParent(this.transform);
                go.transform.localPosition = new Vector2(-25, 0);
                Distribution(ResourceType.Wheat);
                FishButton.interactable = false;
                WheatButton.interactable = false;
                IsDistributed = true;
            });
        }

        public void ManagedUpdate()
        {
            FishButton.interactable = (IsDistributed) ? false : CanDistribution(ResourceType.Fish);
            WheatButton.interactable = (IsDistributed) ? false : CanDistribution(ResourceType.Wheat);
        }

        private bool CanDistribution(ResourceType resourceType)
        {
            var canDistribution = false;
            var playerResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
            if(playerResource[resourceType] > 0)
            {
                canDistribution = !canDistribution;
            }
            return canDistribution;
        }

        public void NonDistribution()
        {
            for(var i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Point, false, PhotonNetwork.LocalPlayer);
            }       
        }

        private void Distribution(ResourceType resourceType)
        {
            Resource.Send(resourceType, false, PhotonNetwork.LocalPlayer);
        }

        public void Reset()
        {
            if(go != null)
            {
                Destroy(go);
            }
            FishButton.interactable = true;
            WheatButton.interactable = true;
            IsDistributed = false;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("Color"))
            {
                WorkerImage.color = PhotonNetwork.LocalPlayer.GetColor();
            }
        }
    }
}