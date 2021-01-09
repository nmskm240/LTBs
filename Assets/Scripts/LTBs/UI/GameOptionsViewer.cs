using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace LTBs.UI
{
    public class GameOptionsViewer : MonoBehaviour 
    {
        [SerializeField]
        private GameObject OptionsButton;
        [SerializeField]
        private Button CloseButton;
        [SerializeField]
        private BoardSelecter BoardSelecter;
        [SerializeField]
        private UseTileSelecter UseTileSelecter;

        private void Awake() 
        {
            Hide();
            OptionsButton.GetComponent<Button>().onClick.AddListener(() => Show());
            CloseButton.onClick.AddListener(() => Hide());
            CloseButton.onClick.AddListener(() =>  BoardSelecter.SetOptionsData());
            CloseButton.onClick.AddListener(() =>  UseTileSelecter.SetOptionsData());
            if(!PhotonNetwork.IsMasterClient)
            {
                OptionsButton.SetActive(false);
            }
        }

        public void Show()
        {
            GetComponent<Canvas>().enabled = true;
        }

        public void Hide()
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}