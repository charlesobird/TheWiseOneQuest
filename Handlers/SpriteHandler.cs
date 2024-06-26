using _WizardSpriteLocations = TheWiseOneQuest.Utils.WIZARD_SPRITE_SHEET_LOCATIONS;

namespace TheWiseOneQuest.Handlers;

public class SpriteHandler
{
    // Storage of the AnimatedSprites within the game e.g. PlayerSprite, EnemySprite
    public Dictionary<string, AnimatedSprite> activeAnimatedSprites = new();
    public Dictionary<string, Animation> wizardAnimations = new();
    public SpriteHandler() { }

    public void CreateWizardAnimations()
    {
        wizardAnimations.Clear();
        Animation wizIdleAnim =
            new("WizIdle", 7, 128, 128, 0, (int)_WizardSpriteLocations.Idle, true)
            {
                FramesPerSecond = 3
            };
        wizardAnimations.Add("Idle", wizIdleAnim);
        Animation wizCastSpellAnim =
            new("WizCastSpell", 9, 128, 128, 0, (int)_WizardSpriteLocations.CastSpell, false)
            {
                FramesPerSecond = 5
            };
        wizardAnimations.Add("CastSpell", wizCastSpellAnim);

        Animation wizDeathAnim =
            new("WizDeath", 6, 128, 128, 0, (int)_WizardSpriteLocations.Death, false)
            {
                FramesPerSecond = 5
            };
        wizardAnimations.Add("Death", wizDeathAnim);
        Animation wizHurtAnim =
            new("WizDeath", 3, 128, 128, 0, (int)_WizardSpriteLocations.Hurt, false)
            {
                FramesPerSecond = 5
            };
        wizardAnimations.Add("Hurt", wizHurtAnim);

        wizardAnimations.Add("DEFAULT_ANIMATION", wizIdleAnim);
    }
    public AnimatedSprite NewAnimatedSprite(SpriteData spriteData)
    {
        AnimatedSprite animatedSprite = new(
            spriteData.Sprite,
            spriteData.Animations,
            spriteData.SpriteSize,
            spriteData.Effect

        )
        {
            Name = spriteData.Name,
            Position = spriteData.StartingPosition
        };

        activeAnimatedSprites.Add(spriteData.Name, animatedSprite);
        return animatedSprite;
    }

    public void ClearAnimatedSprites()
    {
        activeAnimatedSprites.Clear();
    }
    public void Update(GameTime gameTime)
    {
        if (activeAnimatedSprites.Count > 0)
        {
            foreach (KeyValuePair<string, AnimatedSprite> sprite in activeAnimatedSprites)
            {
                sprite.Value.Update(gameTime);
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (activeAnimatedSprites.Count > 0)
        {
            foreach (KeyValuePair<string, AnimatedSprite> sprite in activeAnimatedSprites)
            {
                sprite.Value.Draw(spriteBatch);
            }
        }
    }
}
