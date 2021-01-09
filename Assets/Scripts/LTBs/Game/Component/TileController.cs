using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.Game.UI;
using LTBs.Game.Component;
using LTBs.System;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.Game.Component
{    
    public class TileController : MonoBehaviour, IPointerClickHandler
    {
        private TileInfoViewer TileInfoViewer;

        private void Awake() 
        {
            TileInfoViewer = GameObject.Find("TileInfoViewer").GetComponent<TileInfoViewer>();    
        }

        public void OnPointerClick(PointerEventData e)
        {
            GameObject go = e.pointerEnter;
            if(go != null)
            {
                Tile tile = go.GetComponent<Tile>();
                TileInfoViewer.Show(tile);
                if(tile != null && tile.IsGrassLand && !tile.IsPlayerPresent)
                {
                    switch(PhotonNetwork.LocalPlayer.GetPlayerMove())
                    {
                        case PlayerMoveType.BuildPosSelecting:
                            Builder.Build(PhotonNetwork.LocalPlayer.GetWillBuild(), e.pointerEnter.GetComponent<Tile>().Pos);
                            break;
                        case PlayerMoveType.Work:
                            PhotonNetwork.CurrentRoom.SetWorker(PhotonNetwork.LocalPlayer, e.pointerEnter.GetComponent<Tile>().Pos);
                            PhotonNetwork.LocalPlayer.UseWorker();
                            break;
                    }
                }
            }
        }
    }
}