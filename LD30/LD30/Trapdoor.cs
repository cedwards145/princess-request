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
    public class Trapdoor : MapEntity
    {
        private int switchID;
        private bool open = false;

        public Trapdoor(Game1 game, Map map, Vector2 pos, int id)
            : base(game, map, pos)
        {
            switchID = id;
            affectedByGravity = false;
            entityCollidable = true;

            texture = Textures.getTexture("trapdoor");
            updateCollisionBox();
        }

        public override void update()
        {
            base.update();

            if (Switches.getSwitch(switchID) && !open)
            {
                open = true;
                entityCollidable = false;
                texture = Textures.getTexture("trapdoor-open");

                
            }

        }

    }
}
