using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class ExchangeSelecter : MonoBehaviour 
    {
        [SerializeField]
        private GameObject PResourceDds;
        [SerializeField]
        private GameObject EResourceDds;
        [SerializeField]
        private Button ExchangeButton;

        private List<ResourceType> ExchangeResource = new List<ResourceType>()
        {
            ResourceType.Stone,
            ResourceType.Wood,
            ResourceType.Fish,
            ResourceType.Wheat,
        };
        private List<ResourceType> PlayerResource = new List<ResourceType>();

        private void Awake() 
        {
            GetComponent<Canvas>().enabled = false;
            var options = new List<Dropdown.OptionData>();
            foreach(var resource in ExchangeResource)
            {
                var str = Enum.GetName(typeof(ResourceType), resource);
                var sprite = Resources.Load("Textures/" + str, typeof(Sprite)) as Sprite;
                options.Add(new Dropdown.OptionData(str, sprite));
            }
            foreach(Transform tf in EResourceDds.transform)
            {
                var dd = tf.gameObject.GetComponent<Dropdown>();
                dd.ClearOptions();
                dd.AddOptions(options);
                dd.RefreshShownValue();
            }
            ExchangeButton.onClick.AddListener(() => Exchange());
        }

        public void Show(ExchangeType ExchangeType)
        {
            PlayerResource.Clear();
            string name;
            Sprite sprite;
            var options = new List<Dropdown.OptionData>();
            var pResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.CurrentRoom.GetTurnPlayer().ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
            switch(ExchangeType)
            {
                case ExchangeType.ExchangeTile:
                    PResourceDds.transform.GetChild(0).gameObject.SetActive(true);
                    PResourceDds.transform.GetChild(1).gameObject.SetActive(true);
                    PResourceDds.transform.GetChild(2).gameObject.SetActive(false);
                    EResourceDds.transform.GetChild(0).gameObject.SetActive(true);
                    EResourceDds.transform.GetChild(1).gameObject.SetActive(true);
                    foreach(var resources in pResource)
                    {
                        for(var i = 0; i < resources.Value; i++) 
                        {
                            if(resources.Key == ResourceType.Money || resources.Key == ResourceType.Point)
                            {
                                continue;
                            }
                            name = Enum.GetName(typeof(ResourceType), resources.Key);
                            sprite = Resources.Load("Textures/" + name, typeof(Sprite)) as Sprite;
                            options.Add(new Dropdown.OptionData(name, sprite));
                            PlayerResource.Add(resources.Key);
                        }
                    }

                    break;
                case ExchangeType.MoneyThree:
                    PResourceDds.transform.GetChild(0).gameObject.SetActive(true);
                    PResourceDds.transform.GetChild(1).gameObject.SetActive(true);
                    PResourceDds.transform.GetChild(2).gameObject.SetActive(true);
                    EResourceDds.transform.GetChild(0).gameObject.SetActive(true);
                    EResourceDds.transform.GetChild(1).gameObject.SetActive(false);
                    name = Enum.GetName(typeof(ResourceType), ResourceType.Money);
                    sprite = Resources.Load("Textures/" + name, typeof(Sprite)) as Sprite;
                    options.Add(new Dropdown.OptionData(name, sprite));
                    break;
            }
            for(int i = 0; i < PResourceDds.transform.childCount; i++)
            {
                var dd = PResourceDds.transform.GetChild(i).gameObject.GetComponent<Dropdown>();
                dd.ClearOptions();
                dd.AddOptions(options);
                dd.RefreshShownValue();
            }
            OnValueChanged();
            GetComponent<Canvas>().enabled = true;
        }

        public void OnValueChanged()
        {
            var pResource = GameObject.Find("GamePlayerListViewer").transform.Find("Contents").transform.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).gameObject.GetComponent<GamePlayerNode>().PlayerResource;
            pResource.Remove(ResourceType.Money);
            pResource.Remove(ResourceType.Point);
            for(var i = 0; i < PResourceDds.transform.childCount; i++)
            {
                var dropdownObj = PResourceDds.transform.GetChild(i).gameObject;
                if(dropdownObj.activeSelf)
                {
                    var resourceType = PlayerResource[dropdownObj.GetComponent<Dropdown>().value];
                    pResource[resourceType]--;
                }
            }
            foreach(var resource in pResource)
            {
                if(resource.Value < 0)
                {
                    ExchangeButton.interactable = false;
                    break;
                }
                else
                {
                    ExchangeButton.interactable = true;
                }
            }
        }

        public void Exchange()
        {
            for(var i = 0; i < EResourceDds.transform.childCount; i++)
            {
                var dropdownObj = EResourceDds.transform.GetChild(i).gameObject;
                if(dropdownObj.activeSelf)
                {
                    Resource.Send(ExchangeResource[dropdownObj.GetComponent<Dropdown>().value], true, PhotonNetwork.LocalPlayer);
                }
            }
            for(var i = 0; i < PResourceDds.transform.childCount; i++)
            {
                var dropdownObj = PResourceDds.transform.GetChild(i).gameObject;
                if(dropdownObj.activeSelf)
                {
                    Resource.Send(PlayerResource[dropdownObj.GetComponent<Dropdown>().value], false, PhotonNetwork.LocalPlayer);
                }
            }

            GetComponent<Canvas>().enabled = false;
        }
    }
}