using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheWiseOneQuest.Models.Sprites;

public interface ISprite
{
    Vector2 Position { get; set; }
    Vector2 Size { get; set; }
    int Width { get; }
    int Height { get; }
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
