using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models.Sprites;

namespace TheWiseOneQuest.Models;
public class SpriteData
{
    public string Name;
    public Texture2D Sprite;
    public Dictionary<string, Animation> Animations;
    public Vector2 SpriteSize;
    public Vector2 StartingPosition;
    public SpriteEffects Effect;
    public SpriteData(string name, Texture2D sprite, Dictionary<string, Animation> animations, Vector2 spriteSize,Vector2 startingPosition ,SpriteEffects effect)
    {
        Name = name;
        Sprite = sprite;
        Animations = animations;
        SpriteSize = spriteSize;
        StartingPosition = startingPosition;
        Effect = effect;
    }
}