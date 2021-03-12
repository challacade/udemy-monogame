using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Comora;

namespace rpg
{
    enum Dir {
        Down,
        Up,
        Left,
        Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkRight;
        Texture2D walkLeft;

        Texture2D background;
        Texture2D ball;
        Texture2D skull;

        Player player = new Player();

        Camera camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            this.camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("Player/player");
            walkDown = Content.Load<Texture2D>("Player/walkDown");
            walkRight = Content.Load<Texture2D>("Player/walkRight");
            walkLeft = Content.Load<Texture2D>("Player/walkLeft");
            walkUp = Content.Load<Texture2D>("Player/walkUp");

            background = Content.Load<Texture2D>("background");
            ball = Content.Load<Texture2D>("ball");
            skull = Content.Load<Texture2D>("skull");

            player.animations[0] = new SpriteAnimation(walkDown, 4, 8);
            player.animations[1] = new SpriteAnimation(walkUp, 4, 8);
            player.animations[2] = new SpriteAnimation(walkLeft, 4, 8);
            player.animations[3] = new SpriteAnimation(walkRight, 4, 8);

            player.anim = player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            if (!player.dead)
                Controller.Update(gameTime, skull);

            this.camera.Position = player.Position;
            this.camera.Update(gameTime);

            foreach (Projectile proj in Projectile.projectiles) {
                proj.Update(gameTime);
            }

            foreach (Enemy e in Enemy.enemies) {
                e.Update(gameTime, player.Position, player.dead);
                int sum = 32 + e.radius;
                if (Vector2.Distance(player.Position, e.Position) < sum) {
                    player.dead = true;
                }
            }

            foreach (Projectile proj in Projectile.projectiles) {
                foreach (Enemy enemy in Enemy.enemies) {
                    int sum = proj.radius + enemy.radius;
                    if (Vector2.Distance(proj.Position, enemy.Position) < sum) {
                        proj.Collided = true;
                        enemy.Dead = true;
                    }
                }
            }

            Projectile.projectiles.RemoveAll(p => p.Collided);
            Enemy.enemies.RemoveAll(e => e.Dead);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(this.camera);

            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);
            foreach (Enemy e in Enemy.enemies) {
                e.anim.Draw(_spriteBatch);
            }
            foreach (Projectile proj in Projectile.projectiles)
            {
                _spriteBatch.Draw(ball, new Vector2(proj.Position.X - 48, proj.Position.Y - 48), Color.White);
            }
            if (!player.dead)
                player.anim.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
