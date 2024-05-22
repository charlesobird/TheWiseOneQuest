using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models;
using TheWiseOneQuest.Models.Sprites;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using _WizardSpriteLocations = TheWiseOneQuest.Utils.WIZARD_SPRITE_SHEET_LOCATIONS;

namespace TheWiseOneQuest.Handlers;

public class SpriteHandler
{
    public List<AnimatedSprite> activeAnimatedSprites = new List<AnimatedSprite>();
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
            Position = spriteData.StartingPosition
        };
        activeAnimatedSprites.Add(animatedSprite);
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
            foreach (Sprite sprite in activeAnimatedSprites)
            {
                sprite.Update(gameTime);
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (activeAnimatedSprites.Count > 0)
        {
            foreach (Sprite sprite in activeAnimatedSprites)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
