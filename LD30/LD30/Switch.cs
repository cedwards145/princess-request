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
    public class Switch : MapEntity
    {
        protected int id;

        public Switch(Game1 game, Map map, int switchID)
            : this(game, map, Vector2.Zero, switchID)
        {

        }

        public Switch(Game1 game, Map map, Vector2 pos, int switchID)
            : base(game, map, pos)
        {
            id = switchID;
            texture = Textures.getTexture("switchOff");

            collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public bool getValue()
        {
            return Switches.getSwitch(id);
        }

        public void setValue(bool value)
        {
            Switches.setSwitch(id, value);

            if (value)
            {
                texture = Textures.getTexture("switchOn");
                SoundEffect se = Sounds.getSound("beep");
                se.Play();
            }
            else
                texture = Textures.getTexture("switchOff");
        }

        public void toggle()
        {
            setValue(!Switches.getSwitch(id));
        }
    }
}
