using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Game;

namespace LTBs.Network.CustomProperties.Players
{
    public static class ResourceExtensions
    {
        public static readonly string ResourcePropKey = "Resource";

        public static void SetResource(this Player player, Dictionary<ResourceType, int> resource)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }

            Hashtable playerPros = new Hashtable();
            Dictionary<int, int> tmp = new Dictionary<int, int>();
            foreach(KeyValuePair<ResourceType, int> data in resource)
            {
                tmp.Add((int)data.Key, data.Value);
            }
            playerPros[ResourcePropKey] = tmp;
            player.SetCustomProperties(playerPros);
        }
        
        public static void AddResource(this Player player, Dictionary<ResourceType, int> resource)
        {
            if(player == null || player.CustomProperties == null)
            {
                return;
            }

            Hashtable playerPros = new Hashtable();
            Dictionary<int, int> tmp = new Dictionary<int, int>();
            // Dictionary<ResourceType, int> nowResource = player.GetResource();
            foreach(KeyValuePair<ResourceType, int> data in resource)
            {
                tmp.Add((int)data.Key, data.Value);
            }
            playerPros[ResourcePropKey] = tmp;
            player.SetCustomProperties(playerPros);
        }

        public static Dictionary<ResourceType, int> GetResource(this Player player)
        {
            if(player == null || player.CustomProperties[ResourcePropKey] == null || !player.CustomProperties.ContainsKey(ResourcePropKey))
            {
                return null;
            }

            Dictionary<ResourceType, int> tmp = new Dictionary<ResourceType, int>();
            foreach (KeyValuePair<int, int> data in (Dictionary<int, int>)player.CustomProperties[ResourcePropKey])
            {
                tmp.Add((ResourceType)Enum.ToObject(typeof(ResourceType), data.Key), data.Value);
            }

            return tmp;
        }
    }
}