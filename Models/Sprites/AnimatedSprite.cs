using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheWiseOneQuest.Models.Sprites
{
    public class AnimatedSprite : Sprite
    {
        readonly Dictionary<string, Animation> animations;
        public float LayerDepth;
        string currentAnimation;
        string defaultAnimation;
        bool isAnimating;
        readonly Texture2D texture;

        public SpriteEffects spriteEffect;

        public string CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public string DefaultAnimation
        {
            get { return defaultAnimation; }
            set { defaultAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public AnimatedSprite(
            Texture2D sprite,
            Dictionary<string, Animation> animation,
            Vector2 spriteSize,
            SpriteEffects _spriteEffect = SpriteEffects.None,
            float layerDepth = 0
        )
        {
            LayerDepth = layerDepth;
            texture = sprite;
            animations = new();
            Size = spriteSize;
            Width = (int)spriteSize.X;
            Height = (int)spriteSize.Y;
            spriteEffect = _spriteEffect;
            foreach (string key in animation.Keys)
            {
                animations.Add(key, animation[key]);
            }

            if (animations.ContainsKey("DEFAULT_ANIMATION"))
            {
                currentAnimation = "DEFAULT_ANIMATION";
            }
            isAnimating = true;
        }

        public void PauseAnimation()
        {
            isAnimating = false;
        }
        public void ResetAnimation()
        {
            animations[currentAnimation].Reset();
        }

        public override void Update(GameTime gameTime)
        {
            if (isAnimating)
            {
                animations[currentAnimation].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // if (animations[currentAnimation].CurrentFrame == 0 && !animations[currentAnimation].isLooping)
            // {
            //     currentAnimation = animations.ContainsKey("DEFAULT_ANIMATION") ? "DEFAULT_ANIMATION" : currentAnimation;
            // }
            if (Name.Contains(""))
            spriteBatch.Draw(
                texture,
                new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
                animations[currentAnimation].CurrentFrameRect,
                Color.White,
                0,
                Vector2.Zero,
                spriteEffect,
                LayerDepth
            );
        }
    }

}
