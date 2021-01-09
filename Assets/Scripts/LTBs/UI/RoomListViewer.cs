using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.System;

namespace LTBs.UI
{    
    public class RoomListViewer : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private GameObject Contens;

        private void DestroyAll()
        {
            foreach(Transform tf in Contens.transform)
            {
                Destroy(tf.gameObject);
            }
        }

        private void CreateAllRoomNode(List<RoomInfo> roomList)
        {
            var RoomNodeFactory = new RoomNodeFactory();
            foreach(var room in roomList)
            {
                if(room.PlayerCount <= 0)
                {
                    break;
                }
                var go = RoomNodeFactory.Create(room.Name);
                go.transform.SetParent(Contens.transform);
            }
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log(roomList.Count);
            DestroyAll();
            CreateAllRoomNode(roomList);
        }	
    }
}