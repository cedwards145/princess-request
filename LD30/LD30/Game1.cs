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

namespace LD30
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public StateManager manager;

        public ControllableEntity player;
        public Camera camera;
        public Follower follower;
        public MapState currentMapState = null;

        private Vector2 respawnLocation;

        public SoundEffectInstance theme;

        private int respawnTimer = 120, framesToRespawn = -1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            manager = new StateManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Fonts.loadFonts(this);
            Textures.loadTextures(this);
            Sounds.loadSounds(this);
            Messages.load(this);
            Switches.load();

             theme= Sounds.getSound("theme").CreateInstance();
             theme.IsLooped = true;
             theme.Volume = 0.75f;


             manager.addState(new TitleState(this));

             theme.Play();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // UPDATE INPUT HANDLER
            InputHandler.update();


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || InputHandler.keyPressed(Keys.Escape))
                this.Exit();

            
            // Update the state manager
            manager.update();


            // Respawn logic
            if (framesToRespawn > respawnTimer)
            {
                // If timer is done, respawn player
                player.setPosition(respawnLocation);
                player.controllable = true;
                framesToRespawn = -1;
            }
            else if (framesToRespawn != -1)
                framesToRespawn++;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            spriteBatch.Begin();



            // Draw the state manager
            manager.draw();


            if (follower != null)
                follower.drawOnTop(camera.transformVector(follower.getPosition()));
                        
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void sendMessage(string message, GameState sender)
        {
            currentMapState.enabled = false;
            manager.addState(new MessageState(this, sender, message));
        }

        public void sendMessage(Queue<string> messages, GameState sender)
        {
            currentMapState.enabled = false;
            manager.addState(new MessageState(this, sender, messages));
        }

        public void setRespawn()
        {
            respawnLocation = player.getPosition();
        }

        public void respawn()
        {
            if (framesToRespawn == -1)
            {
                // Start respawn timer
                framesToRespawn = 0;

                // stop the player from moving
                player.controllable = false;

                follower.message();
            }
        }

        public void loadMap(Map newMap)
        {
            if (currentMapState != null)
                manager.removeState(currentMapState);

            currentMapState = new MapState(this, newMap);
            manager.addState(currentMapState);
        }

    }
}
