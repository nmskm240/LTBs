using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using LTBs.Network.CustomProperties.Players;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.UI
{    
    public class RoundCounter : MonoBehaviourPunCallbacks 
    {
        [SerializeField]
        private Text NowRoundText;
        [SerializeField]
        private Text MaxRoundText;

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey("NowRound"))
            {
                NowRoundText.text = PhotonNetwork.CurrentRoom.GetNowRound().ToString();
            }
            if(propertiesThatChanged.ContainsKey("MaxRound"))
            {
                MaxRoundText.text = PhotonNetwork.CurrentRoom.GetMaxRound().ToString();
            }
        }
    }
}