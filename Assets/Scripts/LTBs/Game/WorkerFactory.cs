using UnityEngine;
using LTBs.System;

namespace LTBs.Game
{    
    public class WorkerFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string str = null)
        {
            return Instantiate(Resources.Load("Prefabs/WorkerPrefab") as GameObject);
        }
    }
}