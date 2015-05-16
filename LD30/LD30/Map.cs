using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD30
{
    public class Map : LDComponent
    {
        private Texture2D backTexture, foreTexture, lightTex;
        private List<MapEntity> entities;
        private List<MapEntity> toAdd;
        private List<Rectangle> collidables;
        private Camera camera;

        BlendState multiplyBlend;

        //public Map(Game1 game)
        //    : base(game)
        //{
        //    multiplyBlend = new BlendState();
        //    multiplyBlend.ColorBlendFunction = BlendFunction.Add;
        //    multiplyBlend.ColorSourceBlend = Blend.DestinationColor;
        //    multiplyBlend.ColorDestinationBlend = Blend.Zero;

        //    entities = new List<MapEntity>();
        //    collidables = new List<Rectangle>();
        //    collidables.Add(new Rectangle(0, 467, 2000, 30));
        //    collidables.Add(new Rectangle(0, 337, 200, 30));

        //    backTexture = Textures.getTexture("mapbg");
        //    foreTexture = Textures.getTexture("map");
        //    lightTex = Textures.getTexture("light");

        //    entities.Add(new ControllableEntity(game, this));
        //    camera = new Camera(game, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight);
        //    camera.follow(entities[0]);

        //    Switch s = new Switch(game, this, new Vector2(200, 300), 0);
        //    entities.Add(s);
        //    s = new Switch(game, this, new Vector2(250, 300), 1);
        //    entities.Add(s);

        //    Door d = new Door(game, this, new Vector2(50, 367), 0);
        //    entities.Add(d);
        //    d = new Door(game, this, new Vector2(30, 367), 1);
        //    entities.Add(d);

        //    Spike spike = new Spike(game, this, new Vector2(400, 100));
        //    entities.Add(spike);

        //    game.player = entities[0];
        //    game.camera = camera;
        //    r = new Random();

        //    gameRef.setRespawn();
        //}



        public Map(Game1 game, string filename)
            : base(game)
        {
            // Load data, fg and bg images
            Texture2D mapData = Textures.getTexture(filename);
            foreTexture = Textures.getTexture(filename + "-fg");
            backTexture = Textures.getTexture(filename + "-bg");
            toAdd = new List<MapEntity>();

            // Load light texture and set up blendstates
            lightTex = Textures.getTexture("light");
            multiplyBlend = new BlendState();
            multiplyBlend.ColorBlendFunction = BlendFunction.Add;
            multiplyBlend.ColorSourceBlend = Blend.DestinationColor;
            multiplyBlend.ColorDestinationBlend = Blend.Zero;

            // Set up lists
            entities = new List<MapEntity>();
            collidables = new List<Rectangle>();

            // Create the player
            ControllableEntity player = new ControllableEntity(game, this);
            game.player = player;
            player.setPosition(new Vector2(50, 50));
            entities.Add(player);

            Follower follower = new Follower(game, this, player);
            game.follower = follower;
            entities.Add(follower);

            // Parse data image and spawn entities
            Color[] pixels = new Color[mapData.Width * mapData.Height];
            mapData.GetData(pixels);

            for (int x = 0; x < mapData.Width; x++)
            {
                for (int y = 0; y < mapData.Height; y++)
                {
                    Color currentPixel = pixels[x + (y * mapData.Width)];

                    // Empty Space
                    if (currentPixel.Equals(Color.White))
                        ;
                    // Walls
                    else if (currentPixel.Equals(Color.Black))
                        collidables.Add(new Rectangle(x * 50, y * 50, 50, 50));
                    // Player Spawn
                    else if (currentPixel.Equals(Color.Blue))
                        player.setPosition(new Vector2(x * 50, y * 50));
                    // Spikes
                    else if (currentPixel.Equals(Color.Red))
                        entities.Add(new Spike(game, this, new Vector2(x * 50, y * 50)));
                    // Switches
                    else if (currentPixel.R == 0 && currentPixel.G == 255)
                        entities.Add(new Switch(game, this, new Vector2(x * 50, y * 50), currentPixel.B));
                    // Pressure plates
                    else if (currentPixel.R == 0 && currentPixel.G == 250)
                        entities.Add(new PressurePlate(game, this, new Vector2(x * 50, y * 50), currentPixel.B));
                    // Doors
                    else if (currentPixel.R == 0 && currentPixel.G == 200)
                        entities.Add(new Door(game, this, new Vector2(x * 50, y * 50), currentPixel.B));
                    // Trapdoors
                    else if (currentPixel.R == 0 && currentPixel.G == 150)
                        entities.Add(new Trapdoor(game, this, new Vector2(x * 50, y * 50), currentPixel.B));
                    // Teleporters
                    else if (currentPixel.R == 255 && currentPixel.B == 255)
                        entities.Add(new Teleporter(game, this, new Vector2(x * 50, y * 50), currentPixel.G));
                    // Story Markers
                    else if (currentPixel.R == 100 && currentPixel.G == 0)
                        entities.Add(new StoryMarker(game, this, new Vector2(x * 50, y * 50), currentPixel.B));
                    // Gears
                    else if (currentPixel.R == 0 && currentPixel.G == 100)
                    {
                        // Add wall
                        collidables.Add(new Rectangle(x * 50, y * 50, 50, 50));
                        entities.Add(new Gear(game, this, new Vector2(x * 50, y * 50), currentPixel.B, 0, (x % 2 == 0 ? 0.05f : -0.05f)));
                    }
                }
            }

            // Create the camera
            camera = new Camera(game, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight);
            camera.follow(player);
            game.camera = camera;

            // Set the respawn position
            game.setRespawn();


            r = new Random();
        }


        public override void update()
        {
            base.update();

            foreach (MapEntity e in toAdd)
                entities.Add(e);

            toAdd.Clear();

            foreach (MapEntity entity in entities)
            {
                if (entity.enabled)
                    entity.update();
            }

            if (InputHandler.keyPressed(Keys.M))
                camera.moveTo(new Vector2(100, 100), 30);
            if (InputHandler.keyPressed(Keys.F))
                camera.follow(gameRef.player);


            // CHECK SWITCHES
            if (InputHandler.keyPressed(Keys.Enter))
            {
                foreach (MapEntity entity in entities)
                {
                    if (entity is Switch)
                    {
                        Switch s = (Switch)entity;
                        if (s.intersects(gameRef.player))
                            s.setValue(true);
                    }
                }
            }

            if (InputHandler.keyPressed(Keys.P))
            {
                if (!shake)
                    camera.shake(2);
                else
                    camera.stopShake();
                shake = !shake;
            }            

            camera.update();
        }
        private bool shake = false;

        private Random r;
        public override void draw()
        {
            base.draw();
            Rectangle cameraSize = camera.getSize();
            cameraSize.X /= 2;
            cameraSize.Y /= 2;

            gameRef.spriteBatch.Draw(backTexture, Vector2.Zero, cameraSize, Color.White);

            foreach (MapEntity entity in entities)
            {
                if (entity.visible)
                    entity.drawAt(camera.transformVector(entity.getPosition()));
            }

            gameRef.spriteBatch.End();
            gameRef.spriteBatch.Begin(SpriteSortMode.Immediate, multiplyBlend);

            double multiplier = (r.Next(0, 10) / 100.0) + 1.0;
            int width = (int)(lightTex.Width * multiplier);
            int height = (int)(lightTex.Height * multiplier);

            Rectangle dest = new Rectangle(0 - ((width - lightTex.Width) / 2), 0 - ((height - lightTex.Height) / 2), width, height);

            gameRef.spriteBatch.Draw(lightTex, dest, Color.White * 0.7f);
            gameRef.spriteBatch.End();
            gameRef.spriteBatch.Begin();


            gameRef.spriteBatch.Draw(foreTexture, Vector2.Zero, camera.getSize(), Color.White);
        }

        public List<Rectangle> getCollidables()
        {
            List<Rectangle> collides = new List<Rectangle>();
            foreach (MapEntity e in entities)
            {
                if (e.isEntityCollidable())
                    collides.Add(e.getCollisionBox());
            }

            collides.AddRange(collidables);

            return collides;
            //return collidables;
        }

        public void addEntity(MapEntity e)
        {
            toAdd.Add(e);
        }

    }
}
