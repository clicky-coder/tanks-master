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
    public abstract class Shell
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public SpriteBatch spriteBatch;
        public string description;
        public string specialDescription;
        public bool draw;
        public bool exploding;
        public int initialX;
        public int initialY;
        public int initialMouseX;
        public int initialMouseY;
        public int damage;
        public int weight;
        public int MaxDistance; //distance to go before exploding
        public int MaxDistanceY;
        public int initialVelocity;
        public int x;
        public int counter;
        public int explodeCounter = 0;
        public List<Texture2D> explodeTextures;
        public abstract void Update();
        public  void Draw()
        {
            if (draw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, rectangle, Color.White);
                spriteBatch.End();
            }
        }
    }

    public class Shell_Normal:Shell
    {
        public Shell_Normal(Rectangle r, Texture2D t, SpriteBatch s, int iv, int _initialMouseX, int _initialMouseY, int range)
        {
            rectangle = r;
            texture = t;
            spriteBatch = s;
            damage = 5; //
            weight = 3;
            initialVelocity = iv;
            initialX = rectangle.X;
            initialY = rectangle.Y;
            x = 0;
            counter = 0;
            initialMouseX = _initialMouseX;
            initialMouseY = _initialMouseY;
            MaxDistance = initialX + range;
            
            description = "Normal, everyday artillary shell";
            draw = true;
            exploding = false;
        }
        public override void Update()
        {
            if (g.getDistance(rectangle.X, initialX, rectangle.Y, initialY) >= MaxDistance) Explode();
        }
        public void Explode()
        {
            exploding = true;
            rectangle.Width = rectangle.Height = 100;
            if (explodeCounter < explodeTextures.Count)
            {
                texture = explodeTextures[explodeCounter];
                explodeCounter++;
            }
            else
            {
                draw = false;
            }
        }
        
    }
    public class Shell_Light : Shell
    {
         public Shell_Light(Rectangle r, Texture2D t, SpriteBatch s, int iv, int _initialMouseX, int _initialMouseY, int range)
        {
            rectangle = r;
            texture = t;
            spriteBatch = s;
            damage = 5; //
            weight = 1;
            initialVelocity = iv;
            initialX = rectangle.X;
            initialY = rectangle.Y;
            x = 0;
            counter = 0;
            initialMouseX = _initialMouseX;
            initialMouseY = _initialMouseY;
            MaxDistance = initialX + (range * 10);
            
            description = "Sacrifice damage for increased range";
            draw = true;
            exploding = false;
        }
        public override void Update()
        {
            if (g.getDistance(rectangle.X, initialX, rectangle.Y, initialY) >= MaxDistance) Explode();
        }
        public void Explode()
        {
            exploding = true;
            rectangle.Width = rectangle.Height = 100;
            if (explodeCounter < explodeTextures.Count)
            {
                texture = explodeTextures[explodeCounter];
                explodeCounter++;
            }
            else
            {
                draw = false;
            }
        }
    }
    public class Shell_Heavy : Shell
    {
        public Shell_Heavy(Rectangle r, Texture2D t, SpriteBatch s, int iv, int _initialMouseX, int _initialMouseY, int range)
        {
            rectangle = r;
            texture = t;
            spriteBatch = s;
            damage = 10; //
            weight = 3;
            initialVelocity = iv;
            initialX = rectangle.X;
            initialY = rectangle.Y;
            x = 0;
            counter = 0;
            initialMouseX = _initialMouseX;
            initialMouseY = _initialMouseY;
            MaxDistance = initialX + range;
            
            description = "Sacrifice Range for serious power";
            draw = true;
            exploding = false;
        }
        public override void Update()
        {
            if (g.getDistance(rectangle.X, initialX, rectangle.Y, initialY) >= MaxDistance) Explode();
        }
        public void Explode()
        {
            exploding = true;
            rectangle.Width = rectangle.Height = 100;
            if (explodeCounter < explodeTextures.Count)
            {
                texture = explodeTextures[explodeCounter];
                explodeCounter++;
            }
            else
            {
                draw = false;
            }
        }
    }
}
