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
    public class Teleporter : MapEntity
    {
        private int mapID;

        public Teleporter(Game1 game, Map map, Vector2 pos, int id)
            : base(game, map, pos)
        {
            mapID = id;
            texture = Textures.getTexture("teleport");
            affectedByGravity = false;

            updateCollisionBox();
        }

        public override void update()
        {
            base.update();

            if (InputHandler.keyPressed(Keys.Enter) && gameRef.player.intersects(this))
            {
                gameRef.camera.stopShake();
                gameRef.loadMap(new Map(gameRef, "map" + mapID));
            }
        }

        public override void draw()
        {
            gameRef.spriteBatch.End();

            gameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            base.draw();

            gameRef.spriteBatch.End();

            gameRef.spriteBatch.Begin();

        }
    }
}
