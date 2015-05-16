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
    public class Bullet : MapEntity
    {
        private Vector2 direction;
        private int speed;
        private BulletEmitter emitter;

        public Bullet(Game1 game, Map map, Vector2 pos, Vector2 dir, BulletEmitter e)
            :base(game, map, pos)
        {
            direction = dir;
            texture = Textures.getTexture("bullet");
            affectedByGravity = false;
            updateCollisionBox();
            emitter = e;
        }

        public override void update()
        {
            position += (direction * speed);
            updateCollisionBox();

            List<Rectangle> collidables = containingMap.getCollidables();

            foreach (Rectangle c in collidables)
            {

                if (c.Intersects(collisionBox))
                {
                    emitter.deleteBullet(this);
                }

            }

            // Kill player on contact
            if (intersects(gameRef.player))
                gameRef.respawn();
            

        }
        
    }
}
