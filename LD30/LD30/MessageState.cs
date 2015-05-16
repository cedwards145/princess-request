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
    public class MessageState : GameState
    {
        private string message = null;
        private Queue<string> messages = null;
        private int progress;
        private GameState prevState;
        private SoundEffect se;

        int framesBetweenSound = 5;
        int framesPassed = 0;

        public MessageState(Game1 game, GameState state, string reqMessage)
            : base(game)
        {
            message = reqMessage;
            prevState = state;
            se = Sounds.getSound("message");
        }

        public MessageState(Game1 game, GameState state, Queue<string> messageQueue)
            : base(game)
        {
            prevState = state;
            se = Sounds.getSound("message");
            messages = messageQueue;
            message = messages.Dequeue();
        }

        public override void update()
        {
            base.update();

            if (progress < message.Length)
            {
                progress++;

                framesPassed++;
                if (framesPassed >= framesBetweenSound)
                {
                    se.Play();
                    framesPassed = 0;
                }
            }

            if (InputHandler.keyPressed(Keys.Enter))
            {
                if (progress < message.Length)
                    progress = message.Length;
                // Message finished
                else
                {
                    // Check if there are more messages queued
                    if (messages != null && messages.Count > 0)
                    {
                        progress = 0;
                        message = messages.Dequeue();
                    }
                    // If not, remove state
                    else
                    {
                        gameRef.manager.removeState(this);
                        prevState.enabled = true;
                    }
                }
            }
        }

        public override void draw()
        {
            base.draw();

            gameRef.spriteBatch.DrawString(Fonts.mainFont, message.Substring(0, progress), new Vector2(0, 0), Color.White);
        }
    }
}
