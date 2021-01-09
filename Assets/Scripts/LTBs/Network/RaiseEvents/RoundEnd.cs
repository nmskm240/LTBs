using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using LTBs.Game.UI;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Network.RaiseEvents
{
    public class RoundEnd : RaiseEventPractitioner
    {
        public override void OnEvent(EventData photonEvent)
        {
            if(photonEvent.Code == (byte)RaiseEventType.RoundEnd && this.gameObject.name == "Board")
            {
                foreach(Transform tf in this.transform)
                {
                    if(tf.childCount != 0)
                    {
                        AllChildDestoy(tf);
                    }
                }
                PhotonNetwork.LocalPlayer.SetWorkerNum(PhotonNetwork.CurrentRoom.GetMaxWorker());
            }
        }

        private void AllChildDestoy(Transform transform)
        {
            foreach(Transform tf in transform)
            {
                Destroy(tf.gameObject);
            }
        }
    }
}