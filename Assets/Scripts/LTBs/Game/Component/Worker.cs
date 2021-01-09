using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game.UI;
using LTBs.Network.CustomProperties.Players;

namespace LTBs.Game.Component
{    
    public class Worker : MonoBehaviour 
    {
        private Player owner;

        public Vector2 Pos { get; set; }
        public Player Owner 
        {
            get 
            {
                return owner; 
            }
            set 
            {
                owner = value;
                this.GetComponent<SpriteRenderer>().color = owner.GetColor(); 
            } 
        }

        public List<Tile> GetAroundBuildings()
        {
            var buildings = new List<Tile>();
            var board = this.transform.root.gameObject;
            var boardGridLength = board.GetComponent<GridLayoutGroup>().constraintCount;
            var aroundPos = new Vector2[,]
            {
                { new Vector2(-1,-1), new Vector2(0,-1), new Vector2(1,-1) },
                { new Vector2(-1,0), new Vector2(0,0), new Vector2(1,0) },
                { new Vector2(-1,1), new Vector2(0,1), new Vector2(1,1) },
            };
            for(var i = 0; i < aroundPos.GetLength(0); i++)
            {
                for(var j = 0; j < aroundPos.GetLength(1); j++)
                {
                    var tmp = Pos + aroundPos[i,j];
                    if(tmp == Pos)
                    {
                        continue;
                    }
                    var targetChildIndex = (int)tmp.y * boardGridLength + (int)tmp.x;
                    if(targetChildIndex < 0 || targetChildIndex >= board.transform.childCount)
                    {
                        continue;
                    }
                    var tile = board.transform.GetChild(targetChildIndex).gameObject.GetComponent<Tile>();
                    if(tmp == tile.Pos && !tile.IsGrassLand && tile.NormalAbility != null)
                    {
                        buildings.Add(tile);
                    }
                }
            }

            return buildings;
        }
    }
}