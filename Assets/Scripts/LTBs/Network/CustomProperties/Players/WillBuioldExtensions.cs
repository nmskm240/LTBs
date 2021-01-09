using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.System;

namespace LTBs.Network.CustomProperties.Players
{
    public static class WillBuildExtensions
    {
        public static readonly string WillBuildPropKey = "WBuild";

        public static void SetWillBuild(this Player player, Tile tile)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }

            Hashtable playerPros = new Hashtable();
            playerPros[WillBuildPropKey] = tile.Id;
            player.SetCustomProperties(playerPros);
        }

        public static Tile GetWillBuild(this Player player)
        {
            if(player == null || player.CustomProperties[WillBuildPropKey] == null || !player.CustomProperties.ContainsKey(WillBuildPropKey))
            {
                return null;
            }

            IFactory<GameObject> tileFactory = new TileFactory();
            int id = (int)player.CustomProperties[WillBuildPropKey];
            GameObject go = tileFactory.Create(id.ToString());
            Tile tile = go.GetComponent<Tile>();
            Object.Destroy(go);
            return tile;
        }
    }
}