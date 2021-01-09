using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class TurnPlayerExtensions
    {
        public static string TurnPlayerPropKey = "TurnP";

        public static void SetTurnPlayer(this Room room, Player player)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable TurnPlayerPros = new Hashtable();
            TurnPlayerPros[TurnPlayerPropKey] = player;
            room.SetCustomProperties(TurnPlayerPros);
        }

        public static Player GetTurnPlayer(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[TurnPlayerPropKey] == null || !room.CustomProperties.ContainsKey(TurnPlayerPropKey))
            {
                return null;
            }

            Player data = (Player)room.CustomProperties[TurnPlayerPropKey];
            return data;
        }
    }
}