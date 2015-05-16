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
    public class Camera : LDComponent
    {
        private Vector2 position;
        private int width, height;
        private MapEntity following = null;
        private Vector2 destination;
        private bool moving;
        private int speed;

        private Random r;
        private bool shakeCamera = false;
        private int shakeStrength = 5;

        public Camera(Game1 game, int reqWidth, int reqHeight)
            : base(game)
        {
            position = Vector2.Zero;
            width = reqWidth;
            height = reqHeight;
            r = new Random();
        }

        public void centerOn(Vector2 focus)
        {
            position.X = focus.X - (width / 2);
            position.Y = focus.Y - (height / 2);
        }

        public void follow(MapEntity entity)
        {
            following = entity;
        }

        public void moveTo(Vector2 dest, int framesToMove)
        {
            destination = dest;
            moving = true;
            following = null;
            speed = framesToMove;
        }

        public Vector2 transformVector(Vector2 other)
        {
            return new Vector2(other.X - position.X, other.Y - position.Y);
        }

        public void shake(int strength)
        {
            shakeCamera = true;
            shakeStrength = (strength > shakeStrength ? strength : shakeStrength);
        }

        public void stopShake()
        {
            shakeCamera = false;
        }

        public override void update()
        {
            base.update();

            if (following != null)
                centerOn(following.getPosition());

            if (moving && !destination.Equals(position))
            {
                position.X += (destination.X - position.X) / speed;
                position.Y += (destination.Y - position.Y) / speed;
            }
            else
            {
                moving = false;
            }

            if (shakeCamera)
            {
                double xMod = r.NextDouble() * (shakeStrength * 2) - shakeStrength;
                double yMod = r.NextDouble() * (shakeStrength * 2) - shakeStrength;
                position.X += (float)xMod;
                position.Y += (float)yMod;
            }
        }


        public Vector2 getPosition()
        {
            return position;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Rectangle getSize()
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}
