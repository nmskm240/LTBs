using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Network.CustomProperties.Rooms;

namespace LTBs.UI
{    
    public class BoardSelecter : MonoBehaviour 
    {
        [SerializeField]
        private Dropdown Dropdown;

        private string DefaultBoard = "9x6 default0";

        private readonly string TargetPath = Application.streamingAssetsPath + "/Board";

        private void Awake() 
        {
            OnValueChanged();
            SetOptionsData();
        }

        public void OnValueChanged()
        {
            Dropdown.ClearOptions();
            var tmp = new List<Dropdown.OptionData>();
            var files = Directory.GetFiles(TargetPath, "*.csv", SearchOption.TopDirectoryOnly);
            foreach(var file in files)
            {
                tmp.Add(new Dropdown.OptionData(Path.GetFileNameWithoutExtension(file)));
            }
            Dropdown.AddOptions(tmp);
            Dropdown.RefreshShownValue();
        }

        public void SetOptionsData()
        {
            var fileName = GameObject.Find("BoardSelecter").transform.Find("BoardDropdown").transform.Find("Label").GetComponent<Text>().text;
            fileName = (fileName == "")? DefaultBoard : fileName;
            var tmp = CSVReader.Read(File.ReadAllText(TargetPath + "/" + fileName + ".csv"));
            Debug.Log(tmp);
            PhotonNetwork.CurrentRoom.SetBoard(tmp);
        }
    }
}