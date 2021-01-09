using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using LTBs.UI;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.RaiseEvents;

namespace LTBs.Game.Component
{
    public class Board : MonoBehaviourPunCallbacks
    {
        private void Awake() 
        {
            var boardData = PhotonNetwork.CurrentRoom.GetBoard();;
            GetComponent<GridLayoutGroup>().constraintCount = boardData[0].Length;
            if(PhotonNetwork.IsMasterClient)
            {
                // string[][] boardData = PhotonNetwork.CurrentRoom.GetBoard();
                // PhotonNetwork.RaiseEvent((byte)RaiseEventType.ChangeBoardLayout, boardData[0].Length, new RaiseEventOptions(){ Receivers = ReceiverGroup.All, }, new SendOptions());
                int i = 0;
                foreach(string[] strs in boardData)
                {
                    int j = 0;
                    foreach(string str in strs)
                    {
                        PhotonNetwork.CurrentRoom.SetTile(str, new Vector2(j, i));
                        j++;
                    }
                    i++;
                }
            }
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("Tile"))
            {
                GameObject go = PhotonNetwork.CurrentRoom.GetTileData();
                Tile tile = go.GetComponent<Tile>();
                Vector2 targetPos = new Vector2();
                bool isChange = false;
                foreach(Transform tf in this.transform)
                {
                    if(tf.gameObject.GetComponent<Tile>().Pos == tile.Pos)
                    {
                        targetPos = tf.gameObject.GetComponent<Tile>().Pos;
                        Destroy(tf.gameObject);
                        isChange = true;
                    }
                }
                go.transform.SetParent(this.transform);
                if(isChange)
                {
                    go.transform.SetSiblingIndex((int)targetPos.y * GetComponent<GridLayoutGroup>().constraintCount + (int)targetPos.x);
                }
            }
            if(propertiesThatChanged.ContainsKey("Worker"))
            {
                GameObject go = PhotonNetwork.CurrentRoom.GetWorker();
                Worker worker = go.GetComponent<Worker>();
                go.transform.SetParent(this.transform.GetChild((int)worker.Pos.y * GetComponent<GridLayoutGroup>().constraintCount + (int)worker.Pos.x));
                go.transform.localPosition = new Vector3(0,0,0);
                if(PhotonNetwork.CurrentRoom.GetTurnPlayer() == PhotonNetwork.LocalPlayer)
                {
                    GameObject.Find("BuildingAbilityListViewer").GetComponent<BuildingAbilityListViewer>().Show(worker.GetAroundBuildings());
                }
            }
        }
     }
}