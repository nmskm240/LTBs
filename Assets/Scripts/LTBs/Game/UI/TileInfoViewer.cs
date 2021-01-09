using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using LTBs.Game.Component;

namespace LTBs.Game.UI
{    
    public class TileInfoViewer : MonoBehaviour 
    {
        [SerializeField]
        private Text NameText;
        [SerializeField]
        private Text AbilityText;
        [SerializeField]
        private Text OwnerText;
        [SerializeField]
        private Text PointText;
        [SerializeField]
        private Image TileImage;

        private void Awake()
        {
            GetComponent<Canvas>().enabled = false;
        }

        public void Show(Tile tile)
        {
            NameText.text = tile.Name;
            AbilityText.text = tile.AbilityText;
            OwnerText.text = (tile.Owner != null) ? tile.Owner.NickName : "";
            PointText.text = tile.Points.ToString();
            TileImage.sprite = tile.Face;
            StartCoroutine(ShowProcess(tile));
        }

        private IEnumerator ShowProcess(Tile tile)
        {
            var canvas = GetComponent<Canvas>();
            canvas.enabled = true;
            yield return new WaitForSeconds(1f);
            canvas.enabled = false;
        }
    }
}