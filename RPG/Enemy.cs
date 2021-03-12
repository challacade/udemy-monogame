using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    class Enemy
    {
        public static List<Enemy> enemies = new List<Enemy>();

        private Vector2 position = new Vector2(0, 0);
        private int speed = 150;
        public SpriteAnimation anim;
        public int radius = 30;
        private bool dead = false;

        public Enemy(Vector2 newPos, Texture2D spriteSheet) {
            position = newPos;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public Vector2 Position {
            get {
                return position;
            }
        }

        public bool Dead {
            get { return dead; }
            set { dead = value; }
        }

        public void Update(GameTime gameTime, Vector2 playerPos, bool isPlayerDead) {
            anim.Position = new Vector2(position.X - 48, position.Y - 66);
            anim.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isPlayerDead) {
                Vector2 moveDir = playerPos - position;
                moveDir.Normalize();
                position += moveDir * speed * dt;
            }
        }
    }
}
