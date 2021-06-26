using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Ship
    {
        static public Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        public int speed = 180;
        public int radius = 30;

        public void shipUpdate(GameTime gameTime) {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right) && position.X < 1280) {
                position.X += speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X -= speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) && position.Y < 720)
            {
                position.Y += speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
            {
                position.Y -= speed * dt;
            }
        }
    }
}
