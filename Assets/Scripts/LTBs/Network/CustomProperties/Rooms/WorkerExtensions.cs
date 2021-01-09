using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.System;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class WorkerExtensions
    {
        public static readonly string WorkerPropKey = "Worker";

        public static void SetWorker(this Room room, Player player, Vector2 workerPos)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable workerPros = new Hashtable();
            workerPros[WorkerPropKey] = player.ActorNumber.ToString() + "," + workerPos.x + "," + workerPos.y;
            room.SetCustomProperties(workerPros);
        }

        public static GameObject GetWorker(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[WorkerPropKey] == null || !room.CustomProperties.ContainsKey(WorkerPropKey))
            {
                return null;
            }

            var WorkerFactory = new WorkerFactory();
            string data = (string)room.CustomProperties[WorkerPropKey];
            List<string> workerD = data.Split(',').ToList();
            GameObject go = WorkerFactory.Create();
            Worker worker = go.GetComponent<Worker>();
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                if(player.ActorNumber == int.Parse(workerD[0]))
                {
                    worker.Owner = player;
                }
            }
            worker.Pos = new Vector2(int.Parse(workerD[1]), int.Parse(workerD[2]));
            return go;
        }
    }
}