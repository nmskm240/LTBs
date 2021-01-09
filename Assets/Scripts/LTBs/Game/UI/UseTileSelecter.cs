using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.Game.UI
{    
    public class UseTileSelecter : MonoBehaviour 
    {
        [SerializeField]
        private Toggle DefaultTileToggel;
        [SerializeField]
        private Toggle RandomTileToggel;

        private List<int> Tiles = new List<int>();  
        private List<int> DefaultTiles = new List<int>()
        {
            4,4,4,4,5,6,7,8,9,10,11,12,13,14,15,16
        };  

        private void Awake() 
        {
            SetOptionsData(); 
        }

        public void SetOptionsData()
        {
            Tiles.Clear();
            if(DefaultTileToggel.isOn)
            {
                Tiles = DefaultTiles;
            }
            else
            {
                for(int i = 0; i < 4; i++)
                {
                    Tiles.Add(4);
                }
                for(int i = 0; i < 12; i++)
                {
                    var id = Random.Range(5, 29);
                    //id:17 邸宅の効果が完成していないためid:17 は除外
                    if(Tiles.Contains(id) || id == 17)
                    {
                        i--;
                        continue;
                    }
                    Tiles.Add(id);
                }
            }
            PhotonNetwork.CurrentRoom.SetBuild(Tiles);
        }
    }
}