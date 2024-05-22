using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using GeonBit.UI;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheWiseOneQuest.Models;
using TheWiseOneQuest.Screens;
using TheWiseOneQuest.Utils;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using _Utils = TheWiseOneQuest.Utils.Utils;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models.Sprites;

namespace TheWiseOneQuest.Handlers;

public class BattleHandler
{
	private bool playerCasting = false;
	private bool enemyCasting = false;
	private EnemyWizard enemyWizard;
	private ProjectileData playerProjectileData;
	private ProjectileData enemyProjectileData;
	private Element playerElement;
	private Element enemyElement;
	private string playerMoveName;
	private string enemyMoveName;
	private AnimatedSprite playerSprite;
	private AnimatedSprite enemySprite;


	public Element PlayerElement
	{
		get { return playerElement; }
		set { playerElement = value; }
	}
	public bool PlayerTurn
	{
		get { return playerCasting; }
		set { playerCasting = value; }
	}
	public bool EnemyTurn
	{
		get { return enemyCasting; }
		set { enemyCasting = value; }
	}

	public BattleHandler() { }

	public void DealDamage() { }

	public double CalculateDamage()
	{
		return 0;
	}
    
	public static string GetSpriteSheetForElement(Element element)
	{
		switch (element)
		{
			case Element.Fire:
				return _Utils.FIRE_WZARD_SPRITE_ATLAS;
			case Element.Air:
				return _Utils.AIR_WIZARD_SPRITE_ATLAS;
			case Element.Water:
				return _Utils.WATER_WIZARD_SPRITE_ATLAS;
			case Element.Earth:
				return _Utils.EARTH_WIZARD_SPRITE_ATLAS;
			default:
				return _Utils.AIR_WIZARD_SPRITE_ATLAS;
		}
	}
	public void CreateSprites()
	{
		Core.spriteHandler.ClearAnimatedSprites();
		Texture2D playerSpriteSheet = _Utils.Content.Load<Texture2D>(
				GetSpriteSheetForElement(playerElement)
			);
		Texture2D enemySpriteSheet = _Utils.Content.Load<Texture2D>(
			GetSpriteSheetForElement(enemyElement)
		);

		playerSprite = Core.spriteHandler.NewAnimatedSprite(new SpriteData(
			"PlayerSprite",
			playerSpriteSheet,
			Core.spriteHandler.wizardAnimations,
			_Utils.WIZARD_SPRITE_SIZE,
			new Vector2(0, (float)(Core.screenHeight * 0.6)),
			SpriteEffects.None
		));
		enemySprite = Core.spriteHandler.NewAnimatedSprite(new SpriteData(
			"EnemySprite",
			enemySpriteSheet,
			Core.spriteHandler.wizardAnimations,
			_Utils.WIZARD_SPRITE_SIZE,
			new Vector2((float)(Core.screenWidth * 0.8), (float)(Core.screenHeight * 0.6)),
			SpriteEffects.FlipHorizontally
		));

	}

	public void PromptElementSelection()
	{
		UserInterface.Active.AddEntity(new ElementSelection());
	}
	public void BattleInit()
	{
		Core.exitGame.Visible = false;
		enemyWizard = Core.wizardHandler.CreateEnemyWizard();
		Element[] elements = Enum.GetValues<Element>();
		enemyElement = elements[_Utils.GenerateRandomInteger(elements.Length)];
		if (enemyWizard.Dexterity > Core.playerWizard.Dexterity)
		{
			enemyCasting = true;
		}
		else if (Core.playerWizard.Dexterity < enemyWizard.Dexterity)
		{
			playerCasting = true;
		}
		else
		{
			while (!enemyCasting && !playerCasting)
			{
				double playerGoesFirst = _Utils.GenerateRandomDouble();
				double enemyGoesFirst = _Utils.GenerateRandomDouble();
				if (playerGoesFirst > enemyGoesFirst)
				{
					playerCasting = true;
				}
				else if (playerGoesFirst < enemyGoesFirst)
				{
					enemyCasting = true;
				}
				else
				{
					continue;
				}
			}
		}
		PromptElementSelection();
	}

	public void StartBattle()
	{
		switch (playerElement)
		{
			case Element.Fire:
				playerMoveName = "Fireball";
				break;
			case Element.Air:
				playerMoveName = "Tornado";
				break;
			case Element.Earth:
				playerMoveName = "Rock Blast";
				break;
			case Element.Water:
				playerMoveName = "Ice Spikes";
				break;
		}
		switch (enemyElement)
		{
			case Element.Fire:
				enemyMoveName = "Fireball";
				break;
			case Element.Air:
				enemyMoveName = "Tornado";
				break;
			case Element.Earth:
				enemyMoveName = "Rock Blast";
				break;
			case Element.Water:
				enemyMoveName = "Ice Spikes";
				break;
		}
		CreateSprites();
		playerProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			playerSprite.Center,
			enemySprite.Position,
			eDirection.Right
		);
		enemyProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			new Vector2(enemySprite.Position.X + 64, enemySprite.Position.Y + 64),
			playerSprite.Position,
			eDirection.Left
		);
		UserInterface.Active.AddEntity(new BattleScreen());
	}

	public void AttackEnemy()
	{

	}

	public void Update()
	{
		// Check if there is a fight
		if (!playerCasting && !enemyCasting) {
			return;
		}
		// A fight is active, so wait for user input to send a projectile
		if (Keyboard.GetState().GetPressedKeys().Contains(Keys.F))
		{
			Console.WriteLine("F clicked");
			Console.WriteLine($"{playerCasting} | {enemyCasting}");
			if (playerCasting)
			{
				Console.WriteLine($"OMG A PROJECTILE THAT IS \"{playerMoveName}\" ");
				Core.projectileHandler.NewElementalMove(playerProjectileData, playerMoveName);
				Core.projectileHandler.FireAllElementalMoves();
				playerCasting = false;
			}
		}
	}
}