using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.Game.UI
{    
    public class PlayerNode : MonoBehaviour 
    {
        [SerializeField]
        private Text PlayerNameText;
        [SerializeField]
        private Image PlayerColor;

        private Player inChargePlayer;

        public Player InChargePlayer 
        {
            set 
            {
                inChargePlayer = value;
                PlayerNameText.text = value.NickName; 
                PlayerColor.color = value.GetColor();
            } 
            get
            {
                return inChargePlayer;
            }
        }
    }
}