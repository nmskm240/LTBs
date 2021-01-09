using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.System;

namespace LTBs.Game.Component
{
    public class Tile : MonoBehaviour
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string AbilityText { get; private set; }
        public int Points { get; private set; }
        public Vector2 Pos { get; set; }
        public Player Owner { get; set; } = null;
        public bool IsBuilded { get; set; }
        public bool IsGrassLand { get { return (Id == 0) ? true : false; } }
        public bool IsPlayerPresent { get { return (this.transform.childCount != 0) ? true : false; } }
        public List<ResourceType> BuildCost { get; private set; }
        public List<ResourceType> AbilityCost { get; private set; }
        public List<ResourceType> AbilityProfit { get; private set; }
        public Sprite Face { get; private set; }

        public delegate void Ability();
        public Ability NormalAbility;
        public Ability RoundEndAbility;
        public Ability GameEndAbility;

        public void Init(string cardId)
        {
            var tileData = Resources.Load("Data/TileData/" + cardId) as TileData;
            Id = tileData.Id;
            Name = tileData.Name;
            AbilityText = tileData.AbilityText;
            Points = tileData.Points;
            BuildCost = tileData.BuildCost;
            AbilityCost = tileData.AbilityCost;
            AbilityProfit = tileData.AbilityProfit;
            Face = Resources.Load("Textures/Tile/" + Id, typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Tile/" + Id, typeof(Sprite)) as Sprite;
            var normal = new NormalAbilityUtil();
            var round = new RoundEndAbilityUtil();
            var game = new GameEndAbilityUtil();
            var tmp = normal.GetType().GetMethod("Tile" + Id);
            if(tmp != null)
            {
                NormalAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), normal, tmp);
            }
            tmp = round.GetType().GetMethod("Tile" + Id);
            if(tmp != null)
            {
                RoundEndAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), round, tmp);
            }
            tmp = game.GetType().GetMethod("Tile" + Id);
            if(tmp != null)
            {
                GameEndAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), game, tmp);
            }
        }
    }
}