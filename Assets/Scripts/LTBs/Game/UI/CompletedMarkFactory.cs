using UnityEngine;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class CompletedMarkFactory : Object, IFactory<GameObject> 
    {
        public GameObject Create(string str = null)
        {
            return Instantiate(Resources.Load("Prefabs/CompledMarkPrefab") as GameObject);
        }
    }
}