using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class PlayerListViewer : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private GameObject Contents;
        [SerializeField]
        private Text RoomNameText;

        private void DestroyAll()
        {
            foreach (Transform node in Contents.transform)
            {
                Destroy(node.gameObject);
            }
        }

        private void CreateAllPlayerNode()
        {
            var PlayerNodeFactory = new PlayerNodeFactory();
            foreach(var player in PhotonNetwork.PlayerList)
            {
                var go = PlayerNodeFactory.Create();
                var node = go.GetComponent<PlayerNode>();
                node.InChargePlayer = player;
                go.transform.SetParent(Contents.transform);
            }  
        }

        private void Awake() 
        {
            CreateAllPlayerNode();
            RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)	
        {
            DestroyAll();
            CreateAllPlayerNode();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            DestroyAll();
            CreateAllPlayerNode();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("Color"))
            {
                DestroyAll();
                CreateAllPlayerNode();
            }
        }
    }
}