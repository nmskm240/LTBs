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
    public static class TileExtensions
    {
        public static readonly string TilePropKey = "Tile";

        public static void SetTile(this Room room, string tileId, Vector2 tilePos)
        {
            if(room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable TilePutPros = new Hashtable();
            TilePutPros[TilePropKey] = tileId + "," + tilePos.x + "," + tilePos.y;
            room.SetCustomProperties(TilePutPros);
        }

        public static GameObject GetTileData(this RoomInfo room)
        {
            if(room == null || room.CustomProperties[TilePropKey] == null || !room.CustomProperties.ContainsKey(TilePropKey))
            {
                return null;
            }

            IFactory<GameObject> TileFactory = new TileFactory();
            string data = (string)room.CustomProperties[TilePropKey];
            List<string> tileD = data.Split(',').ToList();
            GameObject go = TileFactory.Create(tileD[0]);
            Tile tile = go.GetComponent<Tile>();
            tile.Pos = new Vector2(int.Parse(tileD[1]), int.Parse(tileD[2]));
            tile.IsBuilded = true;
            tile.Owner = PhotonNetwork.CurrentRoom.GetTurnPlayer();
            return go;
        }
    }
}