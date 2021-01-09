using UnityEngine;

namespace LTBs.Game.Component
{    
    public class GoalCard : MonoBehaviour 
    {
        private int id;
        private string abilityText;
        private int point;

        public int Id { get { return id; } }
        public string AbilityText { get { return abilityText; } }
        public int Point { get { return point; } }

        public delegate bool CanComplete();
        public CanComplete CanCompleted;
    }
}