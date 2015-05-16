using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace LD30
{
    public class InputHandler
    {
        private static KeyboardState oldState;
        private static KeyboardState newState;

        public static void update()
        {
            oldState = newState;
            newState = Keyboard.GetState();
        }

        public static bool keyDown(Keys key)
        {
            return newState.IsKeyDown(key);
        }

        public static bool keyPressed(Keys key)
        {
            return oldState.IsKeyUp(key) && newState.IsKeyDown(key);
        }

        public static bool keyUp(Keys key)
        {
            return newState.IsKeyUp(key);
        }
    }
}
