using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using LTBs.Game;
using LTBs.UI;
using LTBs.Game.Component;
using LTBs.Network.CustomProperties.Rooms;
using LTBs.Network.CustomProperties.Players;
using LTBs.System;

namespace LTBs.Game.Component
{
    public class NormalAbilityUtil
    {
        public void Tile1()
        {
            Resource.Send(ResourceType.Stone, true, PhotonNetwork.LocalPlayer);
        }

        public void Tile2()
        {
            Resource.Send(ResourceType.Wood, true, PhotonNetwork.LocalPlayer);
        }

        public void Tile3()
        {
            Resource.Send(ResourceType.Fish, true, PhotonNetwork.LocalPlayer);
        }

        public void Tile4()
        {
            Resource.Send(ResourceType.Wheat, true, PhotonNetwork.LocalPlayer);
        }

        public void Tile5()
        {
            Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Wheat, true, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Fish, true, PhotonNetwork.LocalPlayer);
        }

        public void Tile6()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile7()
        {
            Resource.Send(ResourceType.Wheat, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 4; i++)
            {
                Resource.Send(ResourceType.Money, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile8()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Money, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile9()
        {
            GameObject.Find("ExchangeSelecter").GetComponent<ExchangeSelecter>().Show(ExchangeType.ExchangeTile);
        }
                
        public void Tile10()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
            }
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Stone, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile11()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Wood, false, PhotonNetwork.LocalPlayer);
            }
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile12()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Wheat, false, PhotonNetwork.LocalPlayer);
            }
            for(int i = 0; i < 5; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile13()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Stone, false, PhotonNetwork.LocalPlayer);
            }
            for(int i = 0; i < 5; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile14()
        {
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
            }
            for(int i = 0; i < 5; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile18()
        {
            Resource.Send(ResourceType.Wheat, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile19()
        {
            Resource.Send(ResourceType.Fish, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Money, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile20()
        {
            Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Wood, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile21()
        {
            for(int i = 0; i < 2; i++)
            {
                Resource.Send(ResourceType.Fish, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile22()
        {
            Resource.Send(ResourceType.Money, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile23()
        {
            Resource.Send(ResourceType.Wood, false, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Stone, false, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Fish, false, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Wheat, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 7; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile24()
        {
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile25()
        {
            Resource.Send(ResourceType.Fish, false, PhotonNetwork.LocalPlayer);
            Resource.Send(ResourceType.Wheat, false, PhotonNetwork.LocalPlayer);
            for(int i = 0; i < 4; i++)
            {
                Resource.Send(ResourceType.Point, true, PhotonNetwork.LocalPlayer);
            }
        }

        public void Tile26()
        {
            for(int i = 0; i < 3; i++)
            {
                Resource.Send(ResourceType.Money, true, PhotonNetwork.LocalPlayer);
            }
        }
    }
}