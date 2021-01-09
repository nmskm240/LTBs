using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class TurnCycleExtensions
    {
        public static string TurnCyclePropKey = "TurnCycle";

        public static void SetTurnCycle(this Room room, IEnumerable<Player> players)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable TurnCyclePros = new Hashtable();
            TurnCyclePros[TurnCyclePropKey] = players.ToArray();
            room.SetCustomProperties(TurnCyclePros);
        }

        public static List<Player> GetTurnCycle(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[TurnCyclePropKey] == null || !room.CustomProperties.ContainsKey(TurnCyclePropKey))
            {
                return null;
            }

            Player[] data = (Player[])room.CustomProperties[TurnCyclePropKey];
            return data.ToList();
        }
    }
}