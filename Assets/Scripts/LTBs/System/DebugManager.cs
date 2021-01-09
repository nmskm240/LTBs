using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.UI;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Players;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.System;

namespace LTBs.System
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField]
        private bool IsDebugMode = false;

        private readonly int MaxResourceID = 5;
        private readonly int MaxBuildID = 28;

        private GameObject Board;

        private ResourceType resourceType = ResourceType.Wood;
        private int ResourceId = 0;
        private int BuildId = 0;
        private Vector2 BuildPos = Vector2.zero;
        private int RoundNum = 0;
        private int WorkerNum = 0;
        private bool IsGet = false;

        private void Awake() 
        {
            Board = GameObject.Find("Board");    
            RoundNum = PhotonNetwork.CurrentRoom.GetNowRound();
            WorkerNum = PhotonNetwork.LocalPlayer.GetWorkerNum();
        }

        private void OnGUI() 
        {
            if(IsDebugMode)
            {
                GUI.Label(new Rect(0,0,85,20), PhotonNetwork.PlayerList[ResourceId].NickName);
                if(GUI.Button(new Rect(85,0,20,20), ">"))
                {
                    ResourceId = (ResourceId == PhotonNetwork.PlayerList.Length - 1) ? 0 : ResourceId + 1;
                }
                GUI.Label(new Rect(0,20,85,20), "Worker:" + WorkerNum);
                if(GUI.Button(new Rect(85,20,20,20), ">"))
                {
                    WorkerNum = (WorkerNum == PhotonNetwork.CurrentRoom.GetMaxWorker()) ? 0 : WorkerNum + 1;
                }
                if(GUI.Button(new Rect(0,40,85,20), "Set worker"))
                {
                    PhotonNetwork.PlayerList[ResourceId].SetWorkerNum(WorkerNum);
                }
                GUI.Label(new Rect(0,60,200,20), "PlayerMoveType:" + Enum.GetName(typeof(PlayerMoveType), PhotonNetwork.PlayerList[ResourceId].GetPlayerMove()));
                GUI.Label(new Rect(0,80,200,20), "ResourceType:" + Enum.GetName(typeof(ResourceType), resourceType));
                if(GUI.Button(new Rect(150,80,20,20), ">"))
                {
                    var tmp = (int)resourceType;
                    tmp = (tmp == MaxResourceID) ? 0 : tmp + 1;
                    resourceType = (ResourceType)Enum.ToObject(typeof(ResourceType), tmp);
                }
                IsGet = GUI.Toggle(new Rect(0,100,125,20), IsGet, "Is get");
                if(GUI.Button(new Rect(0,120,125,20), "Send resource"))
                {
                    var resourceFactory = new ResourceFactory();
                    var go = resourceFactory.Create(Enum.GetName(typeof(ResourceType), resourceType));
                    var photonview = go.GetComponent<PhotonView>();
                    photonview.RPC("Init", RpcTarget.All, Enum.GetName(typeof(ResourceType), resourceType), IsGet, PhotonNetwork.PlayerList[ResourceId]);
                }
                GUI.Label(new Rect(0,160,85,20), "ID:" + (Resources.Load("Data/TileData/" + BuildId.ToString(), typeof(TileData)) as TileData).Name);
                if(GUI.Button(new Rect(85,160,20,20), ">"))
                {
                    BuildId = (BuildId == MaxBuildID) ? 0 : BuildId + 1;
                }
                GUI.Label(new Rect(0,180,85,20), "x:" + BuildPos.x);
                if(GUI.Button(new Rect(85,180,20,20), ">"))
                {
                    BuildPos.x = (BuildPos.x == Board.GetComponent<GridLayoutGroup>().constraintCount - 1) ? 0 : BuildPos.x + 1;
                }
                GUI.Label(new Rect(0,200,85,20), "y:" + BuildPos.y);
                if(GUI.Button(new Rect(85,200,20,20), ">"))
                {
                    var go = GameObject.Find("Board");
                    BuildPos.y = (BuildPos.y == (go.transform.childCount / go.GetComponent<GridLayoutGroup>().constraintCount) - 1) ? 0 : BuildPos.y + 1;
                }
                if(GUI.Button(new Rect(0,220,85,20), "Build"))
                {
                    PhotonNetwork.CurrentRoom.SetTile(BuildId.ToString(), BuildPos);
                }
                GUI.Label(new Rect(0,260,85,20), "Round:" + RoundNum.ToString());
                if(GUI.Button(new Rect(85,260,20,20), ">"))
                {
                    RoundNum = (RoundNum == PhotonNetwork.CurrentRoom.GetMaxRound()) ? 1 : RoundNum + 1;
                }
                if(GUI.Button(new Rect(0,280,85,20), "Set round"))
                {
                    PhotonNetwork.CurrentRoom.SetNowRound(RoundNum);
                }
            }
        }
    }
}