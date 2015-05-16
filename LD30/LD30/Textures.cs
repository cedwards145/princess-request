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
    public class Textures
    {
        private static Game1 gameRef;

        public static Dictionary<string, Texture2D> textures;

        public static void loadTextures(Game1 game)
        {
            gameRef = game;
            textures = new Dictionary<string, Texture2D>();
        }

        public static Texture2D getTexture(string name)
        {
            try
            {
                if (!textures.ContainsKey(name))
                    textures.Add(name, gameRef.Content.Load<Texture2D>(name));
            }
            catch (Exception ex)
            {
                //Sure this will be fine...
                return getTexture("none");
            }

            return textures[name];
        }
    }
}
