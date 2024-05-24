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
            Position += new Vector2(4 * Speed * +(int)Direction, 0);

            // Check for a wizard at that position
            bool checkHits = false;
            if (Direction == eDirection.Left)
            {
                if (Position.X < newPosition.X + 32)
                {
                    checkHits = true;
                }
            }
            else
            {
                if (Position.X > newPosition.X - 32)
                {
                    checkHits = true;
                }
            }
            if (checkHits)
            {
                var possibleHits = Core.spriteHandler.activeAnimatedSprites.Where(x =>
                    x.Value.Position.X == newPosition.X
                );
                foreach (var hit in possibleHits)
                {
                    if (hit.Key == "PlayerSprite")
                    {
                        Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "Hurt";
                        Core.battleHandler.DealDamageAfterHit(true);
                        isFiring = false;
                        isFinished = true;
                    }
                    else if (hit.Key == "EnemySprite")
                    {
                        Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "Hurt";
                        Core.battleHandler.DealDamageAfterHit();
                        isFiring = false;
                        isFinished = true;
                    }

                }

            }

        }
        base.Update(gameTime);
    }
}
