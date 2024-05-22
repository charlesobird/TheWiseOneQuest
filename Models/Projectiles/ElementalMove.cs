using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models.Sprites;
using Core = TheWiseOneQuest.TheWiseOneQuest;

namespace TheWiseOneQuest.Models;

public class ElementalMove : Projectile
{
    public ElementalMove(
        Texture2D sprite,
        Dictionary<string, Animation> animation,
        Vector2 spriteSize,
        Vector2 positionAfterFire,
        eDirection direction
    )
        : base(sprite, animation, spriteSize, positionAfterFire, direction) { }

    public override void Update(GameTime gameTime)
    {
        // check for collision and move sprite
        if (isFiring)
        {
            Position += new Vector2(3 * Speed * (int)Direction, 0);
            if (Position.X >= newPosition.X && Position.Y == newPosition.Y)
            {
                isFiring = false;
                isFinished = true;

                // Check for a wizard at that position
                Console.WriteLine("CUSTOM COLLISION HAPPNED");

                var possibleHits = Core.spriteHandler.activeAnimatedSprites.Where(x =>
                    x.Position.X == newPosition.X
                );
                foreach (var hit in possibleHits)
                {
                    Console.WriteLine("NO CUSTOM COLLISION HAPPNED");
                    Console.WriteLine($"{hit?.Name} was killed by {Name}");
                    hit.CurrentAnimation = "Death";
                }
            }
        }
        base.Update(gameTime);
    }
}
