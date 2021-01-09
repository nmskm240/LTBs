using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace LTBs.Network.CustomProperties.Players
{
    public static class ColorExtensions
    {
        public static readonly string ColorPropKey = "Color";

        public static void SetColor(this Player player, Color color)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }

            Hashtable playerPros = new Hashtable();
            playerPros[ColorPropKey] = color;
            player.SetCustomProperties(playerPros);
        }

        public static Color GetColor(this Player player)
        {
            if(player == null || player.CustomProperties[ColorPropKey] == null || !player.CustomProperties.ContainsKey(ColorPropKey))
            {
                return new Color(0,0,0,0);
            }

            return (Color)player.CustomProperties[ColorPropKey];
        }
    }
}