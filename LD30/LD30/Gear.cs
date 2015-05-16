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
    public class Gear : MapEntity
    {
        private int switchID;
        private float rotation, rotationPerFrame;

        public Gear(Game1 game, Map map, Vector2 pos, int id, float rot, float rpf)
            : base(game, map, pos)
        {
            rotation = rot;
            rotationPerFrame = rpf;

            texture = Textures.getTexture("gear");
            updateCollisionBox();

            affectedByGravity = false;
            switchID = id;
        }

        public override void update()
        {
            base.update();

            if (Switches.getSwitch(switchID))
            {
                //gameRef.camera.shake(1);
                rotation = (rotation + rotationPerFrame) % MathHelper.TwoPi;
            }
        }

        public override void drawAt(Vector2 destination)
        {
            gameRef.spriteBatch.Draw(texture, new Vector2(destination.X - 22, destination.Y - 22), null, Color.White, rotation, new Vector2(collisionBox.Width / 2.0f, collisionBox.Height / 2.0f), 1, SpriteEffects.None, 0);
        }
    }
}
