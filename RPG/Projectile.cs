using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG
{
    class Projectile
    {
        private Vector2 position;
        private int speed = 800;
        private int radius = 15;
        private Dir direction;
        private bool collided = false;

        public static List<Projectile> projectiles = new List<Projectile>();

        public Projectile(Vector2 newPos, Dir newDir) {
            position = newPos;
            direction = newDir;
        }

        public bool Collided {
            get { return collided; }
            set { collided = value; }
        }

        public Vector2 Position {
            get {
                return position;
            }
        }

        public int Radius {
            get {
                return radius;
            }
        }

        public void Update(GameTime gameTime) {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (direction)
            {
                case Dir.Right:
                    position.X += speed * dt;
                    break;
                case Dir.Left:
                    position.X -= speed * dt;
                    break;
                case Dir.Down:
                    position.Y += speed * dt;
                    break;
                case Dir.Up:
                    position.Y -= speed * dt;
                    break;
                default:
                    break;
            }
        }
    }
}
