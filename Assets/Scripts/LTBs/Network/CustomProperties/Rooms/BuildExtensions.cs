using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class BuildExtensions
    {
        public static readonly string BuildPropKey = "Build";

        public static void SetBuild(this Room room, IEnumerable<int> buildList)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable BuildPros = new Hashtable();
            BuildPros[BuildPropKey] = buildList.ToArray();
            room.SetCustomProperties(BuildPros);
        }

        public static List<int> GetBuild(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[BuildPropKey] == null || !room.CustomProperties.ContainsKey(BuildPropKey))
            {
                return null;
            }

            int[] data = (int[])room.CustomProperties[BuildPropKey];
            return data.ToList();
        }
    }
}