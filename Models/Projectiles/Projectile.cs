using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models.Sprites;

namespace TheWiseOneQuest.Models;

public class Projectile : AnimatedSprite
{
	public bool isFiring = false;
	public bool isFinished = false;
	public Vector2 newPosition;
	public eDirection Direction;

	public Projectile(
		Texture2D sprite,
		Dictionary<string, Animation> animation,
		Vector2 spriteSize,
		Vector2 positionAfterFire,
        eDirection direction
	)
		: base(sprite, animation, spriteSize)
	{
        Direction = direction;
		isFinished = false;
		isFiring = false;
		newPosition = positionAfterFire;
	}

	public void Fire()
	{
		if (isFinished)
			return;
		isFiring = true;
	}
	// This is just a basic collision function
	// Create a new class for unique collisions e.g. elemental moves
	public override void Update(GameTime gameTime)
	{
		// check for collision and move sprite
		if (isFiring)
		{
			Position += new Vector2(3 * Speed * (int)Direction, 0) ;
			if (Position.X >= newPosition.X && Position.Y == newPosition.Y)
			{
				isFiring = false;
				isFinished = true;
				Console.WriteLine("NO CUSTOM COLLISION HAPPNED");
			}
		}
		base.Update(gameTime);
	}
}
