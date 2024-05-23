using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models.Sprites;

namespace TheWiseOneQuest.Models;

public enum eDirection
{
    Left = -1,
    Right = 1
}

public class ProjectileData
{
    public Texture2D Sprite;
    public Dictionary<string, Animation> Animations;
    public Vector2 SpriteSize;
    public Vector2 StartingPosition;
    public Vector2 PositionAfterFire;
    public eDirection Direction;
    public string Name;
    public float LayerDepth;

    public ProjectileData(
        Texture2D sprite,
        Dictionary<string, Animation> animations,
        Vector2 spriteSize,
        Vector2 startingPosition,
        Vector2 positionAfterFire,
        eDirection direction,
        string name,
        float layerDepth = 0
    )
    {
        Name = name;
        Sprite = sprite;
        Animations = animations;
        SpriteSize = spriteSize;
        StartingPosition = startingPosition;
        PositionAfterFire = positionAfterFire;
        Direction = direction;
        LayerDepth = layerDepth;
    }
}
