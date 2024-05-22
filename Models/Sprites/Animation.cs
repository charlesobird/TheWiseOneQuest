using System;
using Microsoft.Xna.Framework;

namespace TheWiseOneQuest.Models.Sprites
{
    public class Animation
	{
        // TODO: Issue I1: Fix the issue with it not liking Animations with 2 sprites, only likes 3 for some reason? 
        // Workaround for the above: either add another sprite (duplicate the first one) OR put 3 as the _frameCount param of the constructor
        // FIXED: I1 is apparently fixed...?
		readonly Rectangle[] frames;
        string id;
		int _frameCount;
		int framesPerSecond;
		TimeSpan frameLength;
		TimeSpan frameTimer;
		int currentFrame;
		int frameWidth;
		int frameHeight;

		public bool isLooping;

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
			set { currentFrame = MathHelper.Clamp(value, 0, frames.Length - 1); }
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


		public Animation(string _id, int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset, bool looping)
		{
            id = _id;
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
    }
}
