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
    class Inventory
    {
        Rectangle rectangle;
        Texture2D texture;
        SpriteBatch spriteBatch;
        KeyboardState oldKeys;
        bool draw;
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public List<Item> items = new List<Item>();
        int topIndex;
        int selectedIndex;
        int visibleCounter;
        public Inventory(Rectangle r, Texture2D t, SpriteBatch sb)
        {
            rectangle = r;
            texture = t;
            spriteBatch = sb;
            topIndex = 0;
            selectedIndex = 0;
            visibleCounter = 0;
            draw = false;
                
        }
        public void Update()
        {
            KeyboardState keys = Keyboard.GetState();
            g.SelectedShell = items[selectedIndex].shellType;
            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                selectedIndex++; 
                draw = true; 
                visibleCounter = 0;
            }
            if (selectedIndex >= items.Count) selectedIndex = 0;
            oldKeys = keys;
            if (draw)
            {
                visibleCounter++;
            }
            if (visibleCounter > 200)
            {
                draw = false;
                visibleCounter = 0;
            }
        }
        public void Draw()
        {
            if (draw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, rectangle, Color.White);
                Rectangle selectorRec = new Rectangle(items[selectedIndex].rectangle.X + 100, items[selectedIndex].rectangle.Y + 25, 75, 50);
                spriteBatch.Draw(Textures["selector"], selectorRec, Color.White);
                string sh = "";
                int shCount = 0;
                switch (g.SelectedShell)
                {
                    case "Shell_Normal":
                        sh = "Normal Shell";
                        shCount = g.normalShells;
                        break;
                    case "Shell_Light":
                        sh = "Light Shell";
                        shCount = g.lightShells;
                        break;
                    case "Shell_Heavy":
                        sh = "Heavy Shell";
                        shCount = g.heavyShells;
                        break;
                }
                spriteBatch.DrawString(g.gameText, sh, new Vector2(250, 50), Color.Red);
                spriteBatch.DrawString(g.gameText, items[selectedIndex].description, new Vector2(250, 75), Color.Black);
                spriteBatch.DrawString(g.gameText, "Stock: " + shCount.ToString(), new Vector2(250, 100), Color.ForestGreen);
                spriteBatch.End();
                int X = 30;
                int Y = 0;
                if (selectedIndex != 0)
                {
                    Y += 25;
                    items[selectedIndex - 1].rectangle.X = X;
                    items[selectedIndex - 1].rectangle.Y = Y;
                    items[selectedIndex - 1].Draw();
                    Y += 50;
                    items[selectedIndex].rectangle.X = X;
                    items[selectedIndex].rectangle.Y = Y;
                    items[selectedIndex].Draw();
                }
                else
                {
                    for (int i = topIndex; i < items.Count && i < 2; i++)
                    {
                        X += 0; //arb
                        if (i == 0)
                        {
                            Y += 25;
                        }
                        else Y += 50;
                        items[i].rectangle.X = X;
                        items[i].rectangle.Y = Y;
                        items[i].Draw();
                    }
                }
            }
        }
    }
    public class Item
    {
        public Rectangle rectangle;
        Texture2D texture;
        Texture2D shellTexture;
        SpriteBatch spriteBatch;
        public string description;
        public string shellType;
        public Item(Rectangle r, Texture2D t, Texture2D st, SpriteBatch sb, string d, string sty)
        {
            rectangle = r;
            texture = t;
            shellTexture = st;
            spriteBatch = sb;
            description = d;
            shellType = sty;
        }
        public void Update()
        {
        }
        public void Draw()
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(texture, rectangle, Color.White);
            Rectangle shellRec = new Rectangle(rectangle.X + 13, rectangle.Y + 30, (int)(0.09 * shellTexture.Width), (int)(0.09 * shellTexture.Height));
            spriteBatch.Draw(shellTexture, shellRec, Color.White);
            spriteBatch.End();
        }
    }
}
