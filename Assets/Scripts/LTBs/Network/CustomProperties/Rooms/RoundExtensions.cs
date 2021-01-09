using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Rooms
{
    public static class RoundExtensions
    {
        public static readonly string NowRoundPropKey = "NowRound";
        public static readonly string MaxRoundPropKey = "MaxRound";

        public static void SetNowRound(this Room room, int roundNum)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable RoundPros = new Hashtable();
            RoundPros[NowRoundPropKey] = roundNum;
            room.SetCustomProperties(RoundPros);
        }

        public static int GetNowRound(this Room room)
        {
            if(room == null || room.CustomProperties[NowRoundPropKey] == null || !room.CustomProperties.ContainsKey(NowRoundPropKey))
            {
                return -1;
            }

            int data = (int)room.CustomProperties[NowRoundPropKey];
            return data;
        }

        public static int CountUpRound(this Room room)
        {
            if(room == null || room.CustomProperties == null)
            {
                return -1;
            }

            Hashtable RoundPros = new Hashtable();
            int roundNum = room.GetNowRound() + 1;
            RoundPros[NowRoundPropKey] = roundNum;
            room.SetCustomProperties(RoundPros);
            return roundNum;
        }

        public static void SetMaxRound(this Room room, int roundNum)
        {
            if (room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable RoundPros = new Hashtable();
            RoundPros[MaxRoundPropKey] = roundNum;
            room.SetCustomProperties(RoundPros);
        }

        public static int GetMaxRound(this Room room)
        {
            if (room == null || room.CustomProperties[MaxRoundPropKey] == null || !room.CustomProperties.ContainsKey(MaxRoundPropKey))
            {
                return -1;
            }

            int data = (int)room.CustomProperties[MaxRoundPropKey];
            return data;
        }
    }
}