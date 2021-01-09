using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.System
{
    public class TurnCycle
    {
        public void CreateCycle()
        {
            List<Player> tmp = PhotonNetwork.PlayerList.ToList();
            tmp = tmp.OrderBy(a => Guid.NewGuid()).ToList();
            PhotonNetwork.CurrentRoom.SetTurnCycle(tmp);
        }

        public Player NextTurnPlayer(Player NowPlayer)
        {
            List<Player> tmpList = PhotonNetwork.CurrentRoom.GetTurnCycle();
            int tmp = (tmpList.IndexOf(NowPlayer) + 1 >= tmpList.Count) ? 0 : tmpList.IndexOf(NowPlayer) + 1;
            return tmpList[tmp];
        }
    }
}