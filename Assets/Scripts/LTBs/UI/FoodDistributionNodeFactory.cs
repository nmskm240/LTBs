using UnityEngine;
using LTBs.System;

namespace LTBs.UI
{    
    public class FoodDistributionNodeFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string str = null)
        {
            return Instantiate(Resources.Load("Prefabs/FoodDistributionNodePrefab") as GameObject);
        }
    }
}