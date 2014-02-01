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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool drawHelp;
        int helpCounter;
        Rectangle skyRec;
        Rectangle sunRec;
        Texture2D sunTex;
        Texture2D groundTex;
        Texture2D skyTex;
        Texture2D backDropTexture;
        Rectangle cursorRec;
        Texture2D cursorTex;
        SpriteFont gameText;
        int MoveCloudsCounter = 0;
        Tank tank;
        Dictionary<string, Texture2D> tankShelltTextures = new Dictionary<string, Texture2D>();

        #region inventory
        Inventory inventory;
        Item testItem;
        #endregion
        KeyboardState oldKeys;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            drawHelp = false;
            helpCounter = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region explode
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0001"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0002"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0003"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0004"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0005"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0006"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0007"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0008"));
            g.explodeTextures.Add(LoadTex("explode/fireball_hit_0009"));
            #endregion
            gameText = this.Content.Load<SpriteFont>("GameText");
            g.gameText = gameText;
            skyTex = LoadTex("background");
            sunTex = LoadTex("sun");
            sunRec = new Rectangle(1120, -20, (int)(sunTex.Width * 0.2), (int)(sunTex.Height * 0.2));
            groundTex = LoadTex("ground");
            skyRec = new Rectangle(0, 0, 1280, 307);
            backDropTexture = LoadTex("background_2");
            cursorRec = new Rectangle(0, 0, 50, 50);
            cursorTex = LoadTex("cursor");
            tank = new Tank(LoadTex("Tanks/tank"), new Rectangle(100, 550, 100, 100), spriteBatch);
            tankShelltTextures.Add("Shell_Normal", LoadTex("Shells/Shell_Normal"));
            tankShelltTextures.Add("Shell_Light", LoadTex("Shells/Shell_Light"));
            tankShelltTextures.Add("Shell_Heavy", LoadTex("Shells/Shell_Heavy"));
            tank.shellTextures = tankShelltTextures;
            #region inventory
            Texture2D incBg = LoadTex("InventoryBackground");
            inventory = new Inventory(new Rectangle(0, 0, (int)(0.3 * incBg.Width), (int)(0.3 * incBg.Height)), incBg, spriteBatch);
            inventory.Textures.Add("selector", LoadTex("selector"));
            inventory.items.Add(new Item(new Rectangle(0,0,100,100), LoadTex("Inventory_Item_Background"), tankShelltTextures["Shell_Normal"], spriteBatch, "Normal, everyday artillary shell", "Shell_Normal"));
            inventory.items.Add(new Item(new Rectangle(0, 0, 100, 100), LoadTex("Inventory_Item_Background"), tankShelltTextures["Shell_Light"], spriteBatch, "Sacrifice damage for Range", "Shell_Light"));
            inventory.items.Add(new Item(new Rectangle(0, 0, 100, 100), LoadTex("Inventory_Item_Background"), tankShelltTextures["Shell_Heavy"], spriteBatch, "Sacrifice Range for Serious Damage", "Shell_Heavy"));
            #endregion 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            tank.Update();
            MouseState mouse = Mouse.GetState();
            cursorRec.X = mouse.X;
            cursorRec.Y = mouse.Y;
            foreach (Shell s in tank.shells)
            {
                s.Update();
            }
            inventory.Update();
           
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.H) && oldKeys.IsKeyUp(Keys.H)) drawHelp = !drawHelp;
            oldKeys = keys;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(skyTex, skyRec, Color.White);
            Rectangle bdRec = new Rectangle(0, 200, 1280, 304);
            Rectangle gRec = new Rectangle(0, 500, 1280, 304);
            Rectangle secondSky = new Rectangle(skyRec.X + 1280, 0, 1280, 307);
            spriteBatch.Draw(skyTex, secondSky, Color.White);
            spriteBatch.Draw(backDropTexture, bdRec, Color.White);
            spriteBatch.Draw(groundTex, gRec, Color.White);
            spriteBatch.Draw(cursorTex, cursorRec, Color.White);
            spriteBatch.Draw(sunTex, sunRec, Color.White);
            spriteBatch.DrawString(g.gameText, "Press \"H\" for help", new Vector2(200, 500), Color.White);
            spriteBatch.End();
            if (MoveCloudsCounter % 3 == 0)
            {
                skyRec.X--;
            }
            MoveCloudsCounter++;
            if (skyRec.X < -1280) skyRec.X = 0;
            tank.Draw();
            foreach (Shell s in tank.shells)
            {
                s.Draw();
            }
            inventory.Draw();
            if (drawHelp)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(LoadTex("controls"), new Rectangle(100, 100, LoadTex("controls").Width, LoadTex("controls").Height), Color.White);
                spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }
        Texture2D LoadTex(string path)
        {
            return this.Content.Load<Texture2D>(path);
        }
    }
}

// I am testing how to do commits!
