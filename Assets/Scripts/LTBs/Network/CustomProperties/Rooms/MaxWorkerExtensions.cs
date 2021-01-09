using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class MaxWorkerExtensions
    {
       public static readonly string MaxWorkerPropKey = "MaxWorker";

        public static void SetMaxWorker(this Room room, int workerNum)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable WorkerPros = new Hashtable();
            WorkerPros[MaxWorkerPropKey] = workerNum;
            room.SetCustomProperties(WorkerPros);
        }

        public static int GetMaxWorker(this Room room)
        {
            if(room == null || room.CustomProperties[MaxWorkerPropKey] == null || !room.CustomProperties.ContainsKey(MaxWorkerPropKey))
            {
                return -1;
            }

            int data = (int)room.CustomProperties[MaxWorkerPropKey];
            return data;
        }
    }
}