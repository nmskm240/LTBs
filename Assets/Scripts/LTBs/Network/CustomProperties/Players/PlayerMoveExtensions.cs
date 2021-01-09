using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.System;

namespace LTBs.Network.CustomProperties.Players
{
    public static class PlayerMoveExtensions
    {
        public static readonly string PlayerMovePropKey = "PMove";
        
        public static void SetPlayerMove(this Player player, PlayerMoveType pmt)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }

            Hashtable playerPros = new Hashtable();
            playerPros[PlayerMovePropKey] = (int)pmt;
            player.SetCustomProperties(playerPros);
        }

        public static PlayerMoveType GetPlayerMove(this Player player)
        {
            if(player == null || player.CustomProperties[PlayerMovePropKey] == null || !player.CustomProperties.ContainsKey(PlayerMovePropKey))
            {
                return PlayerMoveType.None;
            }

            return (PlayerMoveType)Enum.ToObject(typeof(PlayerMoveType), (int)player.CustomProperties[PlayerMovePropKey]);
        }
    }
}