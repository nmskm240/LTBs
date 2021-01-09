using UnityEngine;
using Photon.Pun;
using LTBs.Game.Component;
using LTBs.System;

namespace LTBs.Game
{
    public class TileFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string id)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/TilePrefab") as GameObject);
            Tile tile = go.GetComponent<Tile>();
            tile.Init(id);
            return go;
        }
    }
}