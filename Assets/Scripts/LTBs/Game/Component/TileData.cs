using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTBs.Game;

namespace LTBs.Game.Component
{
    [CreateAssetMenu(fileName = "TileData", menuName = "LTBs/TileData", order = 0)]
    public class TileData : ScriptableObject
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string name;
        [SerializeField]
        private string abilityText;
        [SerializeField]
        private int points;
        [SerializeField]
        private List<ResourceType> buildCost;
        [SerializeField]
        private List<ResourceType> abilityCost;
        [SerializeField]
        private List<ResourceType> abilityProfit;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string AbilityText { get { return abilityText; } }
        public int Points { get { return points; } }
        public List<ResourceType> BuildCost { get { return buildCost; } }
        public List<ResourceType> AbilityCost { get { return abilityCost; } }
        public List<ResourceType> AbilityProfit { get { return abilityProfit; } }
    }
}