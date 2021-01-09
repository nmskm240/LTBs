using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;

namespace LTBs.Network.CustomProperties.Players
{
    public static class WorkerNumExtensions
    {
        public static readonly string WorkerNumPropKey = "WorkerN";

        public static void SetWorkerNum(this Player player, int num)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }
            Hashtable workerNPros = new Hashtable();
            workerNPros[WorkerNumPropKey] = num;
            player.SetCustomProperties(workerNPros);
        }

        public static int GetWorkerNum(this Player player)
        {
            if(player == null || player.CustomProperties[WorkerNumPropKey] == null || !player.CustomProperties.ContainsKey(WorkerNumPropKey))
            {
                return -1;
            }

            return (int)player.CustomProperties[WorkerNumPropKey];
        }

        public static void UseWorker(this Player player)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }
            Hashtable workerNPros = new Hashtable();
            workerNPros[WorkerNumPropKey] = player.GetWorkerNum() - 1;
            player.SetCustomProperties(workerNPros);
        }
    }
}