namespace TheWiseOneQuest.Handlers;

public class ProjectileHandler
{
    // Storage of the elementalMove projectiles
    public static List<ElementalMove> elementalMoves = new();
    // Storage of the projectile animations (each elemental move is an animation)
    public Dictionary<string, Animation> projectileAnimations = new();
    public ProjectileHandler() { }

    // Used to add each projectile animation to the projectileAnimations Dictionary to be used throughout the project
    public void CreateProjectileAnimations()
    {
        // The animation constructor follows the format:
        // id, frameCount, frameWidth, frameHeight, xOffset (where is it on the spritesheet x-axis),, yOffset (where is it on the spritesheet y-axis),  looping (is the animation supposed to loop?)
        Animation fireball = new("Fireball", 2, 64, 64, 0, 0, true) { FramesPerSecond = 1 };
        projectileAnimations.Add("Fireball", fireball);
        Animation tornado = new("Tornado", 4, 64, 64, 0, 64, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Tornado", tornado);
        Animation rocks = new("Rocks", 3, 64, 64, 0, 128, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Rock Blast", rocks);
        Animation iceSpikes = new("Ice Spikes", 3, 64, 64, 0, 192, true) { FramesPerSecond = 2 };
        projectileAnimations.Add("Ice Spikes", iceSpikes);
    }
    // Create and store a new elemental move
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
    // Filter out any completed elemental moves 
    public void ClearFinishedElementalMoves()
    {
        elementalMoves.RemoveAll(x => x.isFinished);
    }
    // Remove all elemental moves, regardless of whether they're finished or not
    public void ClearElementalMoves()
    {
        elementalMoves.Clear();
    }
    // Used to update the animations of the projectiles, as well as their position if they're firing
    public void Update(GameTime gameTime)
    {
        if (elementalMoves.Count > 0)
        {
            for (int i = 0; i < elementalMoves.Count; i++)
            {
                elementalMoves[i].Update(gameTime);
            }
        }
    }
    // Create the projectiles on screen
    public void Draw(SpriteBatch spriteBatch)
    {
        if (elementalMoves.Count > 0)
        {
            for (int i = 0; i < elementalMoves.Count; i++)
            {
                elementalMoves[i].Draw(spriteBatch);
            }
        }
    }
}