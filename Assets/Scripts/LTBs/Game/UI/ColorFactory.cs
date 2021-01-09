using UnityEngine;
using LTBs.System;

namespace LTBs.Game.UI
{    
    public class ColorFactory : IFactory<Color>
    {
        public Color Create(string str = null) 
        {
            return Random.ColorHSV();
        }
    }
}