using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun.UtilityScripts;
using ExitGames.Client.Photon;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.UI;
using LTBs.System;
using LTBs.Network.RaiseEvents;
using LTBs.Network.CustomProperties.Players;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.System
{    
    public class TurnAndRoundManager : MonoBehaviourPunCallbacks, IPunTurnManagerCallbacks, IRoundCallbacks
    {
        [SerializeField]
        private PunTurnManager punTurnManager;
        private IRoundCallbacks roundCallbacks;

        private TurnCycle turnCycle;
        private int Tmp = 0;
        private TurnStartNotify TurnStartNotify;

        public List<Tile> GameEndAbilities = new List<Tile>();
        public List<Tile> RoundEndAbilities = new List<Tile>();

        [PunRPC]
        private void TurnStart()
        {
            punTurnManager.BeginTurn();
        }

        [PunRPC]
        private void RoundEndProcess(Player NextTurnPlayer)
        {
            GameObject.Find("FoodDistributionSelecter").GetComponent<FoodDistributionSelecter>().Show();
            StartCoroutine(RoundEndCoroutine(NextTurnPlayer));
        }

        private IEnumerator RoundEndCoroutine(Player NextTurnPlayer)
        {
            Tmp = 0;
            yield return new WaitWhile(() => { return Tmp < PhotonNetwork.PlayerList.Length; });
            if(PhotonNetwork.CurrentRoom.GetMaxRound() < PhotonNetwork.CurrentRoom.CountUpRound())
            {
                Debug.Log("ゲーム終了");
                StartCoroutine(GameEndCoroutine());
            }
            else
            {
                roundCallbacks.OnRoundBegins(PhotonNetwork.CurrentRoom.GetNowRound(), NextTurnPlayer);
            }
        }

        private IEnumerator GameEndCoroutine()
        {
            Tmp = 0;
            if(PhotonNetwork.IsMasterClient)
            {
                foreach(var tile in GameEndAbilities)
                {
                    tile.GameEndAbility();
                }
            }
            yield return new WaitForSeconds(0.5f);
            foreach(Transform tf in GameObject.Find("Board").transform)
            {
                if(tf.gameObject.GetComponent<Tile>().Owner == PhotonNetwork.LocalPlayer)
                {
                    for(int i = 0; i < tf.gameObject.GetComponent<Tile>().Points; i++)
                    {
                        Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
            var pResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
            for(;;)
            {
                if(pResource[ResourceType.Money] >= 3)
                {
                    Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
                    yield return new WaitForSeconds(0.5f);
                    for(int i = 0; i < 3; i++)
                    {
                        pResource[ResourceType.Money]--;
                        Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                else
                {
                    PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.PointCalComplet);
                    break;
                }
            }
            yield return new WaitWhile(() => { return Tmp < PhotonNetwork.PlayerList.Length; });
            GameObject.Find("RankingViewer").GetComponent<RankingViewer>().Show();
        }

        private void Awake() 
        {
            punTurnManager.TurnManagerListener = this;
            roundCallbacks = this;
            turnCycle = new TurnCycle();
            TurnStartNotify = GameObject.Find("TurnStartNotify").GetComponent<TurnStartNotify>();
        }

        private void Start() 
        {
            if(PhotonNetwork.IsMasterClient)
            {
                int workerNum = 5 + (2 - PhotonNetwork.PlayerList.Length);
                Debug.Log(workerNum);
                turnCycle.CreateCycle();
                PhotonNetwork.CurrentRoom.SetMaxWorker(workerNum);
                PhotonNetwork.CurrentRoom.SetMaxRound(4);
                PhotonNetwork.CurrentRoom.SetNowRound(1);
                foreach(Player player in PhotonNetwork.PlayerList)
                {
                    player.SetWorkerNum(workerNum);
                    player.SetResource(new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Wood, 0},
                        {ResourceType.Stone, 0},
                        {ResourceType.Fish, 0},
                        {ResourceType.Wheat, 0},
                        {ResourceType.Money, 3},
                        {ResourceType.Point, 0},
                    });
                    player.SetPlayerMove(PlayerMoveType.NonTurn);
                }
                GetComponent<PhotonView>().RPC("TurnStart", RpcTarget.All, null);
            }
        }

        public void TurnEnd()
        {
            punTurnManager.SendMove(null, true);
        }

        public void OnTurnBegins(int turn)
        {
            if(turn == 1 && PhotonNetwork.LocalPlayer == PhotonNetwork.CurrentRoom.GetTurnCycle()[0])
            {
                PhotonNetwork.CurrentRoom.SetTurnPlayer(PhotonNetwork.LocalPlayer);
                TurnStartNotify.InChargePlayer = PhotonNetwork.LocalPlayer;
                TurnStartNotify.Show();
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.None);
            }
            else if(PhotonNetwork.LocalPlayer == PhotonNetwork.CurrentRoom.GetTurnPlayer())
            {
                TurnStartNotify.InChargePlayer = PhotonNetwork.LocalPlayer;
                TurnStartNotify.Show();
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.None);
            }
            else
            {
                TurnStartNotify.InChargePlayer = (PhotonNetwork.CurrentRoom.GetTurnPlayer() == null) ? PhotonNetwork.CurrentRoom.GetTurnCycle()[0] : PhotonNetwork.CurrentRoom.GetTurnPlayer();
                TurnStartNotify.Show();
                PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.NonTurn);
            }
        }

        public void OnTurnCompleted(int turn)
        {
            var isRoundEnd = true;
            var nextTurnPlayer = turnCycle.NextTurnPlayer(PhotonNetwork.CurrentRoom.GetTurnPlayer());
            if(nextTurnPlayer.GetWorkerNum() <= 0)
            {
                roundCallbacks.OnRoundCompleted(PhotonNetwork.CurrentRoom.GetNowRound(), nextTurnPlayer);
            }
            else if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.SetTurnPlayer(nextTurnPlayer);
                GetComponent<PhotonView>().RPC("TurnStart", RpcTarget.All, null);
            }
        }

        public void OnPlayerMove(Player player, int turn, object move)
        {

        }

        public void OnPlayerFinished(Player player, int turn, object move)
        {

        }

        public void OnTurnTimeEnds(int turn)
        {

        }

        public void OnRoundBegins(int round, Player nextStartPlayer)
        {
            PhotonNetwork.CurrentRoom.SetTurnPlayer(turnCycle.NextTurnPlayer(nextStartPlayer));
            TurnStart();
        }

        public void OnRoundCompleted(int round, Player nextStartPlayer)
        {
            Debug.Log("ラウンド終了");
            if(PhotonNetwork.IsMasterClient)
            {
                foreach(var tile in RoundEndAbilities)
                {
                    tile.RoundEndAbility();
                }
                PhotonNetwork.RaiseEvent((byte)RaiseEventType.RoundEnd, null, new RaiseEventOptions(){ Receivers = ReceiverGroup.All, }, new SendOptions());
                GetComponent<PhotonView>().RPC("RoundEndProcess", RpcTarget.All, nextStartPlayer);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("PMove"))
            {
                if(targetPlayer.GetPlayerMove() == PlayerMoveType.RoundComplet || targetPlayer.GetPlayerMove() == PlayerMoveType.PointCalComplet)
                {
                    Tmp++;
                }
            }
        }
    }
}