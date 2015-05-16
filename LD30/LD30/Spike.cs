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
    public class Spike : MapEntity
    {
        public Spike(Game1 game, Map map, Vector2 pos)
            : base(game, map, pos)
        {
            texture = Textures.getTexture("spikes");
        }

        public override void update()
        {
            base.update();

            if (gameRef.player.intersects(this))
                gameRef.respawn();
        }
    }
}
