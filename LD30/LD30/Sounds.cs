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
    public class Sounds
    {

        public static Dictionary<string, SoundEffect> sounds;
        private static Game1 gameRef;

        public static void loadSounds(Game1 game)
        {
            sounds = new Dictionary<string, SoundEffect>();
            gameRef = game;
        }

        public static SoundEffect getSound(string name)
        {
            if (!sounds.ContainsKey(name))
                sounds.Add(name, gameRef.Content.Load<SoundEffect>(name));

            return sounds[name];
        }
    }
}
