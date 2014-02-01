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
using System.Timers;

namespace Tanks
{
    class Tank
    {
        #region shell count
        int normalShells = 100;
        int heavyShells = 5;
        int lightShells = 5;
        #endregion
        Rectangle rectangle;
        Texture2D texture;
        SpriteBatch spriteBatch; //spritebatch to handle drawing
        public List<Shell> shells; //list to hold fired shells
        public Dictionary<string, Texture2D> shellTextures;
        MouseState oldMouse;
        int power;
        int range;
        const int GRAVITY = -10;
        public Tank(Texture2D t, Rectangle r, SpriteBatch sb)
        {
            rectangle = r;
            texture = t;
            spriteBatch = sb;
            power = 50;
            shells = new List<Shell>();
        }
        public void Update()
        {
            Move();
            Shoot();
            #region Update shell count
            g.normalShells = normalShells;
            g.lightShells = lightShells;
            g.heavyShells = heavyShells;
            #endregion
        }
        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();
        }
        void Move()
        {
            int moveX = 5;
            int moveY = 5;
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.A)) rectangle.X -= moveX;
            if (keys.IsKeyDown(Keys.D)) rectangle.X += moveX;
            if (keys.IsKeyDown(Keys.W)) rectangle.Y -= moveY;
            if (keys.IsKeyDown(Keys.S)) rectangle.Y += moveY;
            if (rectangle.X <= 0) rectangle.X += 5;
            if (rectangle.X >= (1280 - rectangle.Width)) rectangle.X -= 5;
            if (rectangle.Y <= 450) rectangle.Y += 5;
            if (rectangle.Y >= (720 - rectangle.Width)) rectangle.Y -= 5;
        }
        void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && mouse.X > rectangle.X + rectangle.Width)//limit to just shooting forward
            {
                //Insert something here to check what bullet is in stock
                switch (g.SelectedShell)
                {
                    case "Shell_Normal":
                        if (normalShells > 0)
                        {
                            Shell_Normal shell = new Shell_Normal(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 40, 25, 25), shellTextures["Shell_Normal"], spriteBatch, power - 5, mouse.X, mouse.Y, power - 5);
                            shell.explodeTextures = g.explodeTextures;
                            shells.Add(shell);
                            normalShells--;
                        }
                        else
                        {
                            //handle
                        }
                        break;
                    case "Shell_Light":
                        if (lightShells > 0)
                        {
                            Shell_Light shellLight = new Shell_Light(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 40, 25, 25), shellTextures["Shell_Light"], spriteBatch, power - 1, mouse.X, mouse.Y, power - 1);
                            shellLight.explodeTextures = g.explodeTextures;
                            shells.Add(shellLight);
                            lightShells--;
                        }
                        else
                        {
                            //handle
                        }
                        break;
                    case "Shell_Heavy":
                        if (heavyShells > 0)
                        {
                            Shell_Heavy shellHeavy = new Shell_Heavy(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 40, 25, 25), shellTextures["Shell_Heavy"], spriteBatch, power - 10, mouse.X, mouse.Y, power - 10);
                            shellHeavy.explodeTextures = g.explodeTextures;
                            shells.Add(shellHeavy);
                            heavyShells--;
                        }
                        else
                        {
                            //handle
                        }
                        break;
                }
            
            }
            oldMouse = mouse;
            foreach (Shell s in shells)
            {
                MoveProjectile(s);
            }
        }
        void MoveProjectile(Shell shell)
        {
            if (shell.exploding == false && shell.draw)
            {
                int slopeY = shell.initialMouseY - shell.initialY;
                int slopeX = shell.initialMouseX - shell.initialX;
                int reduceFactor = 20;
                double distance = Math.Sqrt((slopeY * slopeY) + (slopeX * slopeX));
                if (distance <= 10)
                {
                    reduceFactor = 1;
                }
                slopeY /= reduceFactor;
                slopeX /= reduceFactor;

                shell.rectangle.X += slopeX;
                shell.rectangle.Y += slopeY;
            }
        }
        public Rectangle GetPlayerRec()
        {
            return rectangle;

        }
        public Texture2D GetPlayerTex()
        {
            return texture;
        }
        public List<Shell> GetPlayerShells()
        {
            return shells;
        }
    }
}
