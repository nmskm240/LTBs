using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.UI;
using LTBs.System;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.UI
{    
    public class CompletButton : MonoBehaviour 
    {
        public void OnClick(GameObject Contents)
        {
            foreach(Transform tf in Contents.transform)
            {
                var node = tf.gameObject.GetComponent<FoodDistributionNode>();
                if(!node.IsDistributed)
                {
                    node.NonDistribution();
                }
            }
            PhotonNetwork.LocalPlayer.SetPlayerMove(PlayerMoveType.RoundComplet);
        }
    }
}