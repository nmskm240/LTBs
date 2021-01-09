using UnityEngine;
using LTBs.System;

namespace LTBs.UI
{    
    public class ColorFactory : IFactory<Color>
    {
        public Color Create(string str = null) 
        {
            return Random.ColorHSV();
        }
    }
}