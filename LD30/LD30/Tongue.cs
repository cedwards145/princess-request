using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD30
{
    public class Tongue : MapEntity
    {
        float percentage = 0;
        float tongueMod = 0.3f;

        public Tongue(Game1 game, Map map, Vector2 pos)
            : base(game, map, pos)
        {
            texture = Textures.getTexture("tongue");

            affectedByGravity = false;
        }

        public override void update()
        {
            if (percentage == 100)
                tongueMod *= -1;

            percentage = MathHelper.Clamp(percentage + tongueMod, 0, 100);
            updateCollisionBox();

            if (tongueMod < 0 && percentage > 7)
            {
                if (!tinted)
                {
                    tinted = true;
                    gameRef.currentMapState.tintScreen();
                }

                // Move player to tongue
                Vector2 currentPos = gameRef.player.getPosition();

                gameRef.player.changeGravitySetting(false);
                gameRef.player.setPosition(new Vector2(currentPos.X - 0.5f, currentPos.Y - 0.35f));

                gameRef.theme.Pitch *= 0.9f;

            }
            else if (tongueMod < 0 && percentage < 7)
            {
                gameRef.follower.message("Congratulations!");
            }
        }

        private bool tinted = false;

        protected override void updateCollisionBox()
        {
            collisionBox.Width = (int)(texture.Width * (percentage / 100.0));
            collisionBox.Height = (int)(texture.Height * (percentage / 100.0));
        }

        public override void drawAt(Vector2 destination)
        {
            gameRef.spriteBatch.Draw(texture, destination, new Rectangle(texture.Width - collisionBox.Width, texture.Height - collisionBox.Height, collisionBox.Width, collisionBox.Height), Color.White);
        }
    }
}
