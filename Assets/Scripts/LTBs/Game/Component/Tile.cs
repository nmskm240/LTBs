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
        private int id;
        private string name;
        private string abilityText;
        private int points;
        private List<ResourceType> buildCost;
        private List<ResourceType> abilityCost;
        private List<ResourceType> abilityProfit;
        private Sprite face;
        
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string AbilityText { get { return abilityText; } }
        public int Points { get { return points; } }
        public Vector2 Pos { get; set; }
        public Player Owner { get; set; } = null;
        public bool IsBuilded { get; set; }
        public bool IsGrassLand { get { return (id == 0) ? true : false; } }
        public bool IsPlayerPresent { get { return (this.transform.childCount != 0) ? true : false; } }
        public List<ResourceType> BuildCost { get { return buildCost; } }
        public List<ResourceType> AbilityCost { get { return abilityCost; } }
        public List<ResourceType> AbilityProfit { get { return abilityProfit; } }
        public Sprite Face { get { return face; } }

        public delegate void Ability();
        public Ability NormalAbility;
        public Ability RoundEndAbility;
        public Ability GameEndAbility;

        public void Init(string id)
        {
            var tileData = Resources.Load("Data/TileData/" + id) as TileData;
            this.id = tileData.Id;
            name = tileData.Name;
            abilityText = tileData.AbilityText;
            points = tileData.Points;
            buildCost = tileData.BuildCost;
            abilityCost = tileData.AbilityCost;
            abilityProfit = tileData.AbilityProfit;
            face = Resources.Load("Textures/Tile/" + id, typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Tile/" + id, typeof(Sprite)) as Sprite;
            var normal = new NormalAbilityUtil();
            var round = new RoundEndAbilityUtil();
            var game = new GameEndAbilityUtil();
            var tmp = normal.GetType().GetMethod("Tile" + id);
            if(tmp != null)
            {
                NormalAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), normal, tmp);
            }
            tmp = round.GetType().GetMethod("Tile" + id);
            if(tmp != null)
            {
                RoundEndAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), round, tmp);
            }
            tmp = game.GetType().GetMethod("Tile" + id);
            if(tmp != null)
            {
                GameEndAbility = (Ability)Delegate.CreateDelegate(typeof(Ability), game, tmp);
            }
        }
    }
}