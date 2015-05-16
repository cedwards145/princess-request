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
    public class Door : MapEntity
    {
        private bool opening = false, closing = false, isOpen = false;

        private int doorHeight, switchID;

        private bool opensUpwards = true;
        private int openSpeed = 1, closeSpeed = 4;

        public Door(Game1 game, Map map, Vector2 pos, int id)
            : base(game, map)
        {
            position = pos;
            texture = Textures.getTexture("door");
            switchID = id;

            entityCollidable = true;
            affectedByGravity = false;
            doorHeight = texture.Height;

            updateCollisionBox();
        }

        public Door(Game1 game, Map map, Vector2 pos, int id, int reqOpenSpeed, int reqCloseSpeed, bool opensUp)
            : this(game, map, pos, id)
        {
            openSpeed = reqOpenSpeed;
            closeSpeed = reqCloseSpeed;
            opensUpwards = opensUp;
        }
        
        public override void update()
        {
            base.update();

            // If switch on and door closed, open door
            if (Switches.getSwitch(switchID) && !isOpen)
                open();
            // If switch off and door open, close door
            else if (!Switches.getSwitch(switchID) && isOpen)
                close();

            // Update logic for when the door is in the process of opening
            if (opening)
            {
                // Adjust height and collisions
                doorHeight = (int)MathHelper.Clamp(doorHeight - openSpeed, 0, doorHeight);
                updateCollisionBox();

                // Door has stopped opening
                if (doorHeight == 0)
                {
                    gameRef.camera.stopShake();
                    opening = false;
                    isOpen = true;
                }

            }
            // Update logic for closing
            else if (closing)
            {
                doorHeight = (int)MathHelper.Clamp(doorHeight + closeSpeed, doorHeight, texture.Height);
                updateCollisionBox();

                if (doorHeight == texture.Height)
                {
                    gameRef.camera.stopShake();
                    closing = false;
                    isOpen = false;
                }
            }
        }

        protected override void updateCollisionBox()
        {
            base.updateCollisionBox();
            collisionBox.Height = doorHeight;
        }

        public void open()
        {
            opening = true;
            closing = false;
            //entityCollidable = false;

            gameRef.camera.shake(2);
        }

        public void close()
        {
            opening = false;
            closing = true;
            //entityCollidable = true;

            gameRef.camera.shake(2);
        }

        public override void drawAt(Vector2 destination)
        {
            Rectangle drawBox;

            if (opensUpwards)
                drawBox = new Rectangle(0, texture.Height - collisionBox.Height, collisionBox.Width, collisionBox.Height);
            else
                drawBox = new Rectangle(0, 0, collisionBox.Width, collisionBox.Height);

            if (opensUpwards)
                gameRef.spriteBatch.Draw(texture, destination, drawBox, Color.White);
            else
                gameRef.spriteBatch.Draw(texture, new Vector2(destination.X, destination.Y + (texture.Height - collisionBox.Height)), drawBox, Color.White);
        }
    }
}
