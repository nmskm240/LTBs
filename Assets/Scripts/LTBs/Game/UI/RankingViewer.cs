using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.Game.UI
{    
    public class RankingViewer : MonoBehaviour 
    {
        [SerializeField]
        private GameObject Contents;

        private void Awake() 
        {
            GetComponent<Canvas>().enabled = false;    
        }

        private List<Player> CreateRanking()
        {
            var go = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").gameObject;
            var tmp = PhotonNetwork.PlayerList.ToList();
            tmp.Sort((a, b) => go.transform.Find(b.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource[ResourceType.Point] - go.transform.Find(a.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource[ResourceType.Point]);
            return tmp;
        }

        public void Show()
        {
            var rank = 1;
            var factory = new PlayerNodeFactory();
            var ranking = CreateRanking();
            foreach(var player in ranking)
            {
                var go = factory.Create();
                go.GetComponent<PlayerNode>().InChargePlayer = player;
                go.transform.SetParent(Contents.transform);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60);
                rank++;
            }
            GetComponent<Canvas>().enabled = true;
        }
    }
}