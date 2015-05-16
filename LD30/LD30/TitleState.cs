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
    public class TitleState : GameState
    {
        Texture2D backTex;

        public TitleState(Game1 game)
            : base(game)
        {
            backTex = Textures.getTexture("title");
        }

        public override void update()
        {
            base.update();

            if (InputHandler.keyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                SoundEffect se = Sounds.getSound("menu-select");
                se.Play();

                gameRef.manager.removeState(this);
                gameRef.loadMap(new Map(gameRef, "map2"));
            }
        }

        public override void draw()
        {
            base.draw();

            gameRef.spriteBatch.Draw(backTex, Vector2.Zero, Color.White);
        }
    }
}
