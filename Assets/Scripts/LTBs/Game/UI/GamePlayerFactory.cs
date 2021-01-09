using UnityEngine;
using LTBs.System;

namespace LTBs.Game.UI
{
    public class GamePlayerFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string playerName = null)
        {
            return Instantiate(Resources.Load("Prefabs/GamePlayerNodePrefab") as GameObject);            
        }
    }
}