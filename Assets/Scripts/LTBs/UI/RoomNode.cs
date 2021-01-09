using UnityEngine;
using UnityEngine.UI;
using LTBs.Network;

namespace LTBs.UI
{
    public class RoomNode : MonoBehaviour
    {
        [SerializeField]
        private Text RoomNameText;
        [SerializeField]
        private RoomEntry RoomEntry;

        public string RoomName { set { RoomNameText.text = value; } }

        public void Join()
        {
            RoomEntry.Entry(RoomNameText.text);
        }
    }
}