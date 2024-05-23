using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models;
using TheWiseOneQuest.Models.Sprites;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using _Utils = TheWiseOneQuest.Utils.Utils;
using System.Linq;

namespace TheWiseOneQuest.Handlers;

public class ProjectileHandler
{
    public static List<ElementalMove> elementalMoves = new();
    public Dictionary<string, Animation> projectileAnimations = new();
    public ProjectileHandler() { }

    public void CreateProjectileAnimations()
    {
        Animation fireball = new("Fireball", 2, 64, 64, 0, 0, true) { FramesPerSecond = 1 };
        projectileAnimations.Add("Fireball", fireball);
        Animation tornado = new("Tornado", 4, 64, 64, 0, 64, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Tornado", tornado);
        Animation rocks = new("Rocks", 3, 64, 64, 0, 128, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Rock Blast", rocks);
        Animation iceSpikes = new("Ice Spikes", 3, 64, 64, 0, 192, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Ice Spikes", iceSpikes);
    }
    public ElementalMove NewElementalMove(ProjectileData projectileData, string playerMoveName)
    {
        ElementalMove elementalMove =
            new(
                projectileData.Sprite,
                projectileData.Animations,
                projectileData.SpriteSize,
                projectileData.PositionAfterFire,
                projectileData.Direction
            )
            {
                Name = projectileData.Name,
                Position = projectileData.StartingPosition,
                CurrentAnimation = playerMoveName
            };
        elementalMoves.Add(elementalMove);
        return elementalMove;
    }

    public void ClearFinishedElementalMoves()
    {
        elementalMoves.RemoveAll(x => x.isFinished);
    }
    public void FireAllElementalMoves()
    {
        if (elementalMoves.Count > 0)
        {
            foreach (ElementalMove move in elementalMoves)
            {
                move.Fire();
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        if (elementalMoves.Count > 0)
        {
            foreach (ElementalMove move in elementalMoves)
            {
                move.Update(gameTime);
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (elementalMoves.Count > 0)
        {
            foreach (ElementalMove move in elementalMoves)
            {
                move.Draw(spriteBatch);
            }
        }
    }
}
