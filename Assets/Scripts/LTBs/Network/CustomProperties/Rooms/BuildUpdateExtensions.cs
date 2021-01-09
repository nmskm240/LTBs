using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Game.Component;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class BuildUpdateExtensions
    {
        public static readonly string BuildUpdataPropKey = "BuildU";

        public static void SetBuildUpdate(this Room room, int buildId)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable BuildUPros = new Hashtable();
            BuildUPros[BuildUpdataPropKey] = buildId;
            room.SetCustomProperties(BuildUPros);
        }

        public static int GetBuildUpdate(this Room room)
        {
            if(room == null || room.CustomProperties[BuildUpdataPropKey] == null || !room.CustomProperties.ContainsKey(BuildUpdataPropKey))
            {
                return -1;
            }

            int data = (int)room.CustomProperties[BuildUpdataPropKey];
            return data;
        }
    }
}