using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    class Player
    {
        private Vector2 position = new Vector2(500, 300);
        private int speed = 400;
        private Dir direction = Dir.Down;
        private bool isMoving = false;
        private KeyboardState kStateOld = Keyboard.GetState();
        public bool dead = false;

        public SpriteAnimation anim;

        public SpriteAnimation[] animations = new SpriteAnimation[4];

        public Vector2 Position {
            get {
                return position;
            }
        }

        public void setX(float newX) {
            position.X = newX;
        }

        public void setY(float newY) {
            position.Y = newY;
        }

        public void Update(GameTime gameTime) {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            isMoving = false;

            if (kState.IsKeyDown(Keys.Right)) {
                direction = Dir.Right;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Left))
            {
                direction = Dir.Left;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Up))
            {
                direction = Dir.Up;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Down))
            {
                direction = Dir.Down;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Space))
                isMoving = false;

            if (dead)
                isMoving = false;

            if (isMoving)
            {
                switch (direction)
                {
                    case Dir.Right:
                        if (position.X < 1275)
                            position.X += speed * dt;
                        break;
                    case Dir.Left:
                        if (position.X > 225)
                            position.X -= speed * dt;
                        break;
                    case Dir.Down:
                        if (position.Y < 1250)
                            position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        if (position.Y > 200)
                            position.Y -= speed * dt;
                        break;
                }
            }

            anim = animations[(int)direction];

            anim.Position = new Vector2(position.X - 48, position.Y - 48);

            if (kState.IsKeyDown(Keys.Space))
                anim.setFrame(0);
            else if (isMoving)
                anim.Update(gameTime);
            else
                anim.setFrame(1);

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
                Projectile.projectiles.Add(new Projectile(position, direction));

            kStateOld = kState;
        }
    }
}
