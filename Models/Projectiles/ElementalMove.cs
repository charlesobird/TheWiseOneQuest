using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Components;
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
        eDirection direction,
        float layerDepth = 0
    )
        : base(sprite, animation, spriteSize, positionAfterFire, direction, layerDepth) { }

    public override void Update(GameTime gameTime)
    {
        // check for collision and move sprite
        if (isFiring)
        {
            Position += new Vector2(3 * Speed * (int)Direction, 0);
            if (Position.X >= newPosition.X)
            {
                isFiring = false;
                isFinished = true;

                // Check for a wizard at that position
                Console.WriteLine("CUSTOM COLLISION HAPPNED");

                var possibleHits = Core.spriteHandler.activeAnimatedSprites.Where(x =>
                x.Value.Position.X == newPosition.X
                );
                foreach (var hit in possibleHits)
                {
                    Console.WriteLine($"{hit.Key} was hit by {Name}");
                    if (hit.Key == "PlayerSprite")
                    {
                        Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "Hurt";
                        Core.battleHandler.DealDamageAfterHit(true);
                        Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "Idle";
                    }
                    else if (hit.Key == "EnemySprite")
                    {
                        Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "Hurt";
                        Core.battleHandler.DealDamageAfterHit();
                        Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "Idle";
                        Core.battleHandler.PromptElementSelection();
                    }
                }
            }
        }
        base.Update(gameTime);
    }
}
