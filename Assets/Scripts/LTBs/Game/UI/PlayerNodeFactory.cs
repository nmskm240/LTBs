using UnityEngine;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class PlayerNodeFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string str = null)
        {
            return Instantiate(Resources.Load("Prefabs/PlayerNodePrefab") as GameObject);
        }
    }
}