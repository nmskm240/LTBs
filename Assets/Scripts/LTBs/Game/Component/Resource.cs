using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.Game.UI;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.Game.Component
{
    public class Resource : MonoBehaviour
    {
        private List<Vector3> PlayerNodePos;
        private float Speed = 0.075f;
        private bool IsGet = true;
        private Vector3 TargetPos = Vector3.zero;
        private Player TargetPlayer;
        private  ResourceType ResourceType;

        [PunRPC]
        public void Init(string resourceName, bool isGet, Player targetPlayer)
        {
            IsGet = isGet;
            ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resourceName, true);
            GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/" + Enum.GetName(typeof(ResourceType), ResourceType), typeof(Sprite)) as Sprite;
            TargetPlayer = targetPlayer;
            GetComponent<PhotonView>().TransferOwnership(TargetPlayer.ActorNumber);
            if(IsGet)
            {
                TargetPos = PlayerNodePos[GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(targetPlayer.ActorNumber.ToString()).GetSiblingIndex()];
            }
            else
            {
                TargetPos = Vector3.zero;
                this.transform.position = PlayerNodePos[GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(targetPlayer.ActorNumber.ToString()).GetSiblingIndex()];
                if(TargetPlayer == PhotonNetwork.LocalPlayer)
                {
                    PhotonNetwork.LocalPlayer.AddResource(new Dictionary<ResourceType, int>()
                    {
                        {ResourceType, -1}
                    }); 
                }
            }
        }

        private void Awake() 
        {
            PlayerNodePos = new ResourceRoute().SearchPlayerNodePos(PhotonNetwork.PlayerList.Length);
            // GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.CurrentRoom.GetTurnPlayer().ActorNumber);
        }

        private void Update() 
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed);
            if(transform.position == TargetPos && GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        private void OnDestroy() 
        {
            if(TargetPlayer == PhotonNetwork.LocalPlayer && IsGet)
            {
                PhotonNetwork.LocalPlayer.AddResource(new Dictionary<ResourceType, int>()
                {
                    {ResourceType, 1}
                });
            }
        }

        public static void Send(string resource, bool isGet, Player player)
        {
            var resourceFactory = new ResourceFactory();
            var go = resourceFactory.Create(resource);
            var photonview = go.GetComponent<PhotonView>();
            photonview.RPC("Init", RpcTarget.All, resource, isGet, player);
        }

        public static void Send(ResourceType resource, bool isGet, Player player)
        {
            var resourceFactory = new ResourceFactory();
            var go = resourceFactory.Create(Enum.GetName(typeof(ResourceType), resource));
            var photonview = go.GetComponent<PhotonView>();
            photonview.RPC("Init", RpcTarget.All, Enum.GetName(typeof(ResourceType), resource), isGet, player);
        }

        public static void Send(ResourceType resource, Player sendPlayer, Player receivePlayer)
        {
            Send(resource, false, sendPlayer);
            Send(resource, true, receivePlayer);
        }

        public static void Send(string resource, Player sendPlayer, Player receivePlayer)
        {
            Send(resource, false, sendPlayer);
            Send(resource, true, receivePlayer);
        }
    }
}