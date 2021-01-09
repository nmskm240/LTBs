using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class BoardExtensions
    {
        public static readonly string BoardPropKey = "Board";

        public static void SetBoard(this Room room, IEnumerable<string[]> boards)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable boardPros = new Hashtable();
            boardPros[BoardPropKey] = boards.ToArray();
            room.SetCustomProperties(boardPros);
        }

        public static string[][] GetBoard(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[BoardPropKey] == null || !room.CustomProperties.ContainsKey(BoardPropKey))
            {
                return null;
            }

            string[][] data = (string[][])room.CustomProperties[BoardPropKey];
            return data;
        }
    }
}