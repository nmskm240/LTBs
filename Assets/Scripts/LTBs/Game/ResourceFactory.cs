using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game.Component;
using LTBs.System;

namespace LTBs.Game
{
    public class ResourceFactory : UnityEngine.Object, IFactory<GameObject>
    {
        public GameObject Create(string resourceName)
        {
            // GameObject go = Instantiate(Resources.Load("Prefabs/ResourceNodePrefab") as GameObject);
            GameObject go = PhotonNetwork.Instantiate("Prefabs/ResourceNodePrefab", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            return go;
        }
    }
}