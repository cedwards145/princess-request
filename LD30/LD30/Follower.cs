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
    public class Follower : MapEntity
    {
        private string[] messages;

        private MapEntity target;
        private int speed = 30;

        private double sizeMultiplier = 1.0;
        private double multiplierChange = 0.01;

        // Message stuff
        private int framesForMessage = 300;
        private int framesSinceMessage = -1;
        private int messageIndex = -1;
        private float opacity = 1.0f;

        private string currentMessage = "";

        Random r;

        public Follower(Game1 game, Map map, MapEntity toFollow)
            : base(game, map)
        {
            target = toFollow;
            texture = Textures.getTexture("wisp");

            affectedByGravity = false;

            r = new Random();
            messages = new string[] { "Ouch!", "That looks bad!", "You OK?", "Gosh, be careful!", "Did that hurt?", "Dying sure looks painful" };
        }

        public override void update()
        {
            position.X += (target.getPosition().X - position.X) / speed;
            position.Y += (target.getPosition().Y - position.Y) / speed;

            // Increase framecount if time has not passed,
            // fade message if it has
            if (framesSinceMessage < framesForMessage)
            {
                framesSinceMessage++;
            }
            else
            {
                opacity = MathHelper.Clamp(opacity - 0.01f, 0, opacity);
                if (currentMessage.Equals("Congratulations!") && opacity == 0.0f)
                {
                    gameRef.manager.removeState(gameRef.currentMapState);
                    gameRef.manager.addState(new TitleState(gameRef));
                }
            }

        }

        public override void drawAt(Vector2 destination)
        { }

        public void drawOnTop(Microsoft.Xna.Framework.Vector2 destination)
        {
            gameRef.spriteBatch.End();

            gameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            sizeMultiplier += multiplierChange;

            if (sizeMultiplier > 1.25 || sizeMultiplier < 0.75)
                multiplierChange *= -1;

            int width = (int)(texture.Width * sizeMultiplier);
            int height = (int)(texture.Height * sizeMultiplier);

            Rectangle dest = new Rectangle((int)destination.X - ((width - texture.Width) / 2), (int)destination.Y - ((height - texture.Height) / 2), width, height);

            gameRef.spriteBatch.Draw(texture, dest, Color.White);


            gameRef.spriteBatch.End();

            gameRef.spriteBatch.Begin();

            SpriteFont f = Fonts.mainFont;
            Vector2 size = f.MeasureString(currentMessage);
            gameRef.spriteBatch.DrawString(f, currentMessage, new Vector2(destination.X - (size.X / 2), destination.Y - 20), Color.White * opacity);
        }



        public void message()
        {
            messageIndex = r.Next(0, messages.Length);
            framesSinceMessage = 0;
            opacity = 1.0f;

            currentMessage = messages[messageIndex];
        }

        public void message(string newMessage)
        {
            messageIndex = -1;
            framesSinceMessage = 0;
            opacity = 1.0f;

            currentMessage = newMessage;
        }
    }
}
