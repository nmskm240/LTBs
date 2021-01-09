using ExitGames.Client.Photon;
using UnityEngine;

namespace LTBs.System
{
    public static class CustomTypesRegister
    {
        private static readonly byte[] bufferColor = new byte[4];

        public static void Register() 
        {
            PhotonPeer.RegisterType(typeof(Color), 1, SerializeColor, DeserializeColor);
        }

        private static short SerializeColor(StreamBuffer outStream, object customObject) 
        {
            Color32 color = (Color)customObject;
            lock (bufferColor) {
                bufferColor[0] = color.r;
                bufferColor[1] = color.g;
                bufferColor[2] = color.b;
                bufferColor[3] = color.a;
                outStream.Write(bufferColor, 0, 4);
            }
            return 4;
        }

        private static object DeserializeColor(StreamBuffer inStream, short length) 
        {
            Color32 color = new Color32();
            lock (bufferColor) {
                inStream.Read(bufferColor, 0, 4);
                color.r = bufferColor[0];
                color.g = bufferColor[1];
                color.b = bufferColor[2];
                color.a = bufferColor[3];
            }
            return (Color)color;
        }
    }
}