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
    public class PressurePlate : Switch
    {

        public PressurePlate(Game1 game, Map map, Vector2 pos, int switchId)
            : base(game, map, pos, switchId)
        {
            texture = Textures.getTexture("pressure-plate");
        }

        public override void update()
        {
            base.update();

            if (gameRef.player.intersects(this) && !Switches.getSwitch(id))
                setValue(true);

            if (texture.Name != "pressure-plate")
                texture = Textures.getTexture("pressure-plate");
        }
   }
}
