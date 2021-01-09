using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.Game.Component
{
    public class GameEndAbilityUtil
    {
        public void Tile16()
        {
            Player owner = null;
            var board = GameObject.Find("Board");
            var tiles = new List<Tile>();
            var boardGridLength = board.GetComponent<GridLayoutGroup>().constraintCount;
            var count = 0;
            var pos = Vector2.zero;
            var aroundPos = new Vector2[,]
            {
                { new Vector2(-1,-1), new Vector2(0,-1), new Vector2(1,-1) },
                { new Vector2(-1,0), new Vector2(0,0), new Vector2(1,0) },
                { new Vector2(-1,1), new Vector2(0,1), new Vector2(1,1) },
            };
            foreach(Transform tf in board.transform)
            {
                if(tf.gameObject.GetComponent<Tile>().Id == 16)
                {
                    owner = tf.gameObject.GetComponent<Tile>().Owner;
                    pos = tf.gameObject.GetComponent<Tile>().Pos;
                    break;
                }
            }
            for(var i = 0; i < aroundPos.GetLength(0); i++)
            {
                for(var j = 0; j < aroundPos.GetLength(1); j++)
                {
                    var tmp = pos + aroundPos[i,j];
                    if(tmp == pos)
                    {
                        continue;
                    }
                    var targetChildIndex = (int)tmp.y * boardGridLength + (int)tmp.x;
                    if(targetChildIndex < 0 || targetChildIndex >= board.transform.childCount)
                    {
                        continue;
                    }
                    var tile = board.transform.GetChild(targetChildIndex).gameObject.GetComponent<Tile>();
                    if(tmp == tile.Pos && !tile.IsGrassLand && tile.Owner == owner)
                    {
                        count++;
                    }
                }
            }
            for(int i = 0; i < count; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Resource.Send(ResourceType.Point, true, owner);
                }
            }
        }

        public void Tile28()
        {
            Player owner = null;
            var board = GameObject.Find("Board");
            var tiles = new List<Tile>();
            var boardGridLength = board.GetComponent<GridLayoutGroup>().constraintCount;
            var count = 0;
            var pos = Vector2.zero;
            var aroundPos = new Vector2[,]
            {
                { new Vector2(-1,-1), new Vector2(0,-1), new Vector2(1,-1) },
                { new Vector2(-1,0), new Vector2(0,0), new Vector2(1,0) },
                { new Vector2(-1,1), new Vector2(0,1), new Vector2(1,1) },
            };
            foreach(Transform tf in board.transform)
            {
                if(tf.gameObject.GetComponent<Tile>().Id == 28)
                {
                    owner = tf.gameObject.GetComponent<Tile>().Owner;
                    pos = tf.gameObject.GetComponent<Tile>().Pos;
                    break;
                }
            }
            for(var i = 0; i < aroundPos.GetLength(0); i++)
            {
                for(var j = 0; j < aroundPos.GetLength(1); j++)
                {
                    var tmp = pos + aroundPos[i,j];
                    if(tmp == pos)
                    {
                        continue;
                    }
                    var targetChildIndex = (int)tmp.y * boardGridLength + (int)tmp.x;
                    if(targetChildIndex < 0 || targetChildIndex >= board.transform.childCount)
                    {
                        continue;
                    }
                    var tile = board.transform.GetChild(targetChildIndex).gameObject.GetComponent<Tile>();
                    if(tmp == tile.Pos && tile.IsGrassLand)
                    {
                        count++;
                    }
                }
            }
            for(int i = 0; i < count; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    Resource.Send(ResourceType.Point, true, owner);
                }
            }
        }
    }
}