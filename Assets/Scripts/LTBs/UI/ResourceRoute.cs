using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LTBs.UI
{
    public class ResourceRoute
    {
        private Dictionary<int, List<Vector3>> PlayerNodePos = new Dictionary<int, List<Vector3>>()
        {
            {2, new List<Vector3>(){new Vector3(6.25f, 2.5f, 0), new Vector3(6.25f, 1.4f, 0)}},
            {3, new List<Vector3>(){new Vector3(6.25f, 2.8f, 0), new Vector3(6.25f, 1.7f, 0), new Vector3(6.25f, 0.6f, 0)}},
            {4, new List<Vector3>(){new Vector3(6.25f, 3.5f, 0), new Vector3(6.25f, 2.4f, 0), new Vector3(6.25f, 1.3f, 0), new Vector3(6.25f, 0.2f, 0)}},
        };

        public List<Vector3> SearchPlayerNodePos(int playerCount)
        {
            return PlayerNodePos[playerCount];
        }
    }
}