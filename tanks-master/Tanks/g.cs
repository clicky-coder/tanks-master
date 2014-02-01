using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tanks
{
    public static class g
    {
        public static List<Texture2D> explodeTextures = new List<Texture2D>();
        public static SpriteFont gameText;
        public static string SelectedShell = "";
        #region shellCount
        public static int normalShells = 100;
        public static int heavyShells = 5;
        public static int lightShells = 5;
        #endregion
        public static double getDistance(double x1, double x2, double y1, double y2)
        {
            double Xpart = Math.Pow(x1 - x2, 2);
            double Ypart = Math.Pow(y2 - y1, 2);
            double ans = Math.Sqrt(Xpart + Ypart);
            return ans;
        }
    }
}
