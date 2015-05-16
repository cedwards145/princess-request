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
    public class MapEntity : LDComponent
    {
        protected Vector2 position, velocityVector, accelerationVector;

        protected Texture2D texture;
        protected Map containingMap;
        protected Rectangle collisionBox;
        protected bool entityCollidable = false;

        protected bool affectedByGravity = true;

        protected bool canBounce = false, canJump = true;

        protected SoundEffectInstance collideSE;

        public MapEntity(Game1 game, Map map)
            : this(game, map, Vector2.Zero)
        { }

        public MapEntity(Game1 game, Map map, Vector2 pos)
            : base(game)
        {
            position = pos;
            texture = Textures.getTexture("none");
            containingMap = map;

            accelerationVector = Vector2.Zero;
            velocityVector = Vector2.Zero;

            collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            collideSE = Sounds.getSound("collide").CreateInstance();
        }

        public override void update()
        {
            base.update();

            List<Rectangle> collidables = containingMap.getCollidables();

            if (affectedByGravity)
            {
                velocityVector.Y -= Physics.GRAVITY_ACCELERATION;
                position.Y -= velocityVector.Y;

                updateCollisionBox();

                foreach (Rectangle c in collidables)
                {
                    if (c.Intersects(collisionBox))
                    {
                        // If character moved up into the box
                        if (velocityVector.Y > 0)
                        {
                            position.Y = c.Bottom;
                            updateCollisionBox();
                        }
                        // Otherwise, character moved down into box
                        else
                        {
                            canJump = true;
                            position.Y = c.Top - collisionBox.Height;
                            updateCollisionBox();
                        }

                        if (canBounce)
                        {
                            velocityVector.Y *= Physics.RESTITUTION_COEFFICIENT;

                            canBounce = false;
                        }
                        else
                        {
                            velocityVector.Y = 0;
                        }

                    }
                }


            }
        }

        public void changeGravitySetting(bool value)
        {
            affectedByGravity = value;
        }

        protected virtual void updateCollisionBox()
        {
            collisionBox.X = (int)position.X;
            collisionBox.Y = (int)position.Y;
            collisionBox.Width = texture.Width;
            collisionBox.Height = texture.Height;
        }

        protected bool standingOn(Rectangle rect)
        {
            return (collisionBox.Bottom == rect.Top) && (collisionBox.Left <= rect.Right && collisionBox.Right >= rect.Left);
        }

        public bool intersects(Rectangle other)
        {
            return other.Intersects(collisionBox);
        }

        public bool intersects(MapEntity other)
        {
            return other.collisionBox.Intersects(collisionBox);
        }

        public override void draw()
        {
            drawAt(position);
        }

        public virtual void drawAt(Vector2 destination)
        {
            base.draw();
            gameRef.spriteBatch.Draw(texture, destination, Color.White);
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public bool isEntityCollidable()
        {
            return entityCollidable;
        }

        public Rectangle getCollisionBox()
        {
            return collisionBox;
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

    }
}
