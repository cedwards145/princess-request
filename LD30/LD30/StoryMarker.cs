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
    class StoryMarker : MapEntity
    {
        private int switchID;

        public StoryMarker(Game1 game, Map map, Vector2 pos, int id)
            : base(game, map, pos)
        {
            affectedByGravity = false;
            collisionBox.Width = 50;
            collisionBox.Height = 50;

            switchID = id;
        }

        public override void update()
        {
            if (gameRef.player.intersects(this) && Switches.getSwitch(switchID))
            {
                Messages.getStoryMessage();
                Switches.setSwitch(switchID, false);
            }
        }
    }
}
