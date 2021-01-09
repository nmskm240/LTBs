using UnityEngine;
using LTBs.System;

namespace LTBs.Network
{
    public class RoomNodeFactory : Object, IFactory<GameObject>
    {
        public GameObject Create(string roomName)
        {
            GameObject tmp = Instantiate(Resources.Load("Prefabs/RoomNodePrefab") as GameObject);
            tmp.GetComponent<RoomNode>().RoomName = roomName;
            return tmp;
        }
    }
}