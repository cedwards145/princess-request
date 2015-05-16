using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    public class LDComponent
    {
        protected Game1 gameRef;
        public bool visible = true, enabled = true;

        public LDComponent(Game1 game)
        {
            gameRef = game;
        }

        public virtual void update()
        {         }

        public virtual void draw()
        { }

    }
}
