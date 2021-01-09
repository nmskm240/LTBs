using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using LTBs.System;

namespace LTBs.Game.Component
{
    public class RoundEndAbilityUtil : Object
    {
        public void Tile17()
        {
            
        }

        public void Tile27()
        {
            Player owner = null;
            var board = GameObject.Find("Board");
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
                if(tf.gameObject.GetComponent<Tile>().Id == 27)
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
                    if(tmp == tile.Pos && tile.IsPlayerPresent)
                    {
                        count++;
                    }
                }
            }
            for(int i = 0; i < count; i++)
            {
                Resource.Send(ResourceType.Point, true, owner);
            }
        }
    }
}