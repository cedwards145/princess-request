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
    public class BulletEmitter : MapEntity
    {
        private int frequency, framesSinceLastEmit = 0;
        private List<Bullet> bullets;
        private List<Bullet> toRemove;


        public BulletEmitter(Game1 game, Map map, Vector2 pos, int fireFrequency)
            : base(game, map, pos)
        {
            affectedByGravity = false;

            bullets = new List<Bullet>();
            toRemove = new List<Bullet>();
            frequency = fireFrequency;
        }

        public override void update()
        {
            base.update();

            if (framesSinceLastEmit >= frequency)
            {
                framesSinceLastEmit = 0;

            }

            foreach (Bullet b in bullets)
                b.update();

            foreach (Bullet b in toRemove)
                bullets.Remove(b);
            toRemove.Clear();
        }

        public override void draw()
        {
            base.draw();

            foreach (Bullet b in bullets)
                b.draw();
        }

        public void deleteBullet(Bullet b)
        {
            toRemove.Add(b);
        }

    }
}
