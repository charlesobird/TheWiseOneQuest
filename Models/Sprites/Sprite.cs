using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheWiseOneQuest.Models.Sprites
{
    public enum Facing { Up, Down, Left, Right }

    public abstract class Sprite // : ISprite
    {
        protected float _speed;
        protected Vector2 _velocity;

        public Facing Facing { get; set; }
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Point Tile { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(_speed, 1.0f, 400.0f); }
        }
        public Vector2 Center
        {
            get { return Position + new Vector2(Width / 2, Height / 2); }
        }
        public Vector2 Origin
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }
        public Sprite()
        {
            Position = new Vector2(0);
            Speed = 1.6f;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}