using System;
using Microsoft.Xna.Framework;

namespace TheWiseOneQuest.Models.Sprites
{
    public class Animation
	{
		readonly Rectangle[] frames;
		int _frameCount;
		int framesPerSecond;
		TimeSpan frameLength;
		TimeSpan frameTimer;
		int currentFrame;
		int frameWidth;
		int frameHeight;

		bool isLooping;

        public int FramesPerSecond
		{
			get { return framesPerSecond; }
			set
			{
				if (value < 1)
					framesPerSecond = 1;
				else if (value > 60)
					framesPerSecond = 60;
				else
					framesPerSecond = value;
				frameLength = TimeSpan.FromSeconds(1 / (double)framesPerSecond);
			}
		}

		public Rectangle CurrentFrameRect
		{
			get { return frames[currentFrame]; }
		}

		public int CurrentFrame
		{
			get { return currentFrame; }
			set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1); }
		}

		public int FrameCount
		{
			get { return _frameCount; }
		}

		public int FrameWidth
		{
			get { return frameWidth; }
		}

		public int FrameHeight
		{
			get { return frameHeight; }
		}


		public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset, bool looping)
		{
			frames = new Rectangle[frameCount];
			this.frameWidth = frameWidth;
			this.frameHeight = frameHeight;
			_frameCount = frames.Length;
			for (int i = 0; i < frameCount; i++)
			{
				frames[i] = new Rectangle(
					xOffset + (frameWidth * i),
					yOffset,
					frameWidth,
					frameHeight
				);
			}
			FramesPerSecond = 3;
			isLooping = looping;
			Reset();
		}

        public void Update(GameTime gameTime)
		{

			frameTimer += gameTime.ElapsedGameTime;

			if (frameTimer >= frameLength)
			{
				frameTimer = TimeSpan.Zero;
				currentFrame = (currentFrame + 1) % frames.Length;
				if (isLooping) {
					currentFrame = (currentFrame + 1) % frames.Length;
				} else {
					currentFrame = Math.Min(currentFrame + 1, frames.Length - 1);
				}
			}
		}

		public void Reset()
		{
			currentFrame = 0;
			frameTimer = TimeSpan.Zero;
		}

		// public object Clone()
		// {
		// 	Animation animationClone =
		// 		new(this) { frameWidth = frameWidth, frameHeight = frameHeight };

		// 	animationClone.Reset();

		// 	return animationClone;
		// }

	}
}
