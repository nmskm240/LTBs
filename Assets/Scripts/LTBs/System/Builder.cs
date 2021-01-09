using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.Game.UI;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.System
{
    public static class Builder
    {
        public static void Build(Tile targetTile, Vector2 Pos)
        {
            foreach(var resourceType in targetTile.BuildCost)
            {
                Resource.Send(resourceType, false, PhotonNetwork.LocalPlayer);
            }
            if(targetTile.GameEndAbility != null)
            {
                var turnManager = GameObject.Find("TurnManager").GetComponent<TurnAndRoundManager>();
                turnManager.GameEndAbilities.Add(targetTile);
            }
            if(targetTile.RoundEndAbility != null)
            {
                var turnManager = GameObject.Find("TurnManager").GetComponent<TurnAndRoundManager>();
                turnManager.RoundEndAbilities.Add(targetTile);
            }
            PhotonNetwork.CurrentRoom.SetTile(targetTile.Id.ToString(), Pos);
            PhotonNetwork.CurrentRoom.SetBuildUpdate(targetTile.Id);
            PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.NonTurn);
            PhotonNetwork.LocalPlayer.UseWorker();
        }

        public static void BuildSetup(Tile targetTile)
        {
            if(CanBuild(targetTile))
            {
                // Debug.Log("建築可");
                PhotonNetwork.LocalPlayer.SetWillBuild(targetTile);
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.BuildPosSelecting);
            }
            else
            {
                // Debug.Log("建築不可");
            }
        }

        public static bool CanBuild(Tile targetTile)
        {
            var canBuild = true;
            var playerResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
            var buildCost = new Dictionary<ResourceType, int>();
            foreach(var rt in targetTile.BuildCost)
            {
                if(buildCost.ContainsKey(rt))
                {
                    buildCost[rt]++;
                }
                else
                {
                    buildCost.Add(rt, 1);
                }
            }

            foreach(var cost in buildCost)
            {
                if(cost.Value > playerResource[cost.Key])
                {
                    canBuild = false;
                    break;
                }
            }
            return canBuild;
        }
    }
}