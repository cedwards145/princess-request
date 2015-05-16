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
    public class MapState : GameState
    {
        protected Map map;

        public MapState(Game1 game, Map reqMap)
            : base(game)
        {
            map = reqMap;
            black = Textures.getTexture("black");
        }

        public void tintScreen()
        {
            tint += 0.01f;
        }

        public override void update()
        {
            base.update();

            if (InputHandler.keyPressed(Keys.L))
            {
                //gameRef.sendMessage("Test message! Maybe a slightly longer one...", this);
                Queue<string> queue = new Queue<string>();
                queue.Enqueue("This is a series of messages.");
                queue.Enqueue("I'm not exactly sure how this'll work...");
                queue.Enqueue("Hopefully everything goes according to plan!");
                gameRef.sendMessage(queue, this);
            }
                

            map.update();


            if (tint > 0)
                tint = MathHelper.Clamp(tint + 0.005f, 0, 1);
        }


        private Texture2D black;
        private float tint = 0.0f;


        public override void draw()
        {
            base.draw();

            map.draw();

            gameRef.spriteBatch.Draw(black, new Rectangle(0, 0, gameRef.graphics.PreferredBackBufferWidth, gameRef.graphics.PreferredBackBufferHeight), Color.White * tint);

        }

        public void addEntity(MapEntity e)
        {
            map.addEntity(e);
        }
    }
}
