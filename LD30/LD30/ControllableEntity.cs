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
    public class ControllableEntity : MapEntity
    {
        public bool controllable = true;

        public ControllableEntity(Game1 game, Map map)
            : base(game, map)
        {
            texture = Textures.getTexture("player");
        }

        public override void update()
        {
            base.update();

            if (controllable)
            {
                float xMove = 0;
                if (InputHandler.keyDown(Keys.A))
                    xMove = -7;
                if (InputHandler.keyDown(Keys.D))
                    xMove = 7;

                position.X += xMove;
                updateCollisionBox();

                List<Rectangle> collidables = containingMap.getCollidables();
                // TEST HORIZONTAL COLLISIONS
                foreach (Rectangle c in collidables)
                {

                    if (c.Intersects(collisionBox))
                    {
                        // If character moved right into the box
                        if (xMove > 0)
                            position.X = c.Left - collisionBox.Width;
                        // Otherwise, character moved left into box
                        else if (xMove < 0)
                            position.X = c.Right;
                    }

                }

                if (InputHandler.keyPressed(Keys.W) && canJump)
                {
                    velocityVector.Y = 20;
                    canBounce = true;
                    canJump = false;
                }
            }
        }
    }
}
