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
    public class Fonts
    {
        public static SpriteFont mainFont;

        public static void loadFonts(Game1 game)
        {
            mainFont = game.Content.Load<SpriteFont>(@"mainFont");
        }
    }
}
