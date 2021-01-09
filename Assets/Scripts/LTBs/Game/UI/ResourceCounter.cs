using UnityEngine;
using UnityEngine.UI;

namespace LTBs.Game.UI
{    
    public class ResourceCounter : MonoBehaviour 
    {
        [SerializeField]
        private Text Index;

        public int Count { get; private set; } = 0;

        public void Set(int index)
        {
            Count += index;
            SetText();
        }

        private void SetText()
        {
            Index.text = "x" + Count;
        }
    }
}