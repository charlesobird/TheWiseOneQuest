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
using System.Collections.Generic;
using System.Threading;
using TheWiseOneQuest.Components;
using GeonBit.UI.Entities;
using System.Threading.Tasks;

namespace TheWiseOneQuest.Handlers;

public enum BlockType
{
	COMPLETE,
	GLANCING_BLOW
}

public class BattleHandler
{
	private BattleScreen battleScreen;
	private bool playerCasting = false;
	private bool enemyCasting = false;
	private bool playerBlocking = false;
	private bool enemyBlocking = false;
	private ProjectileData playerProjectileData;
	private ProjectileData enemyProjectileData;
	private Element playerElement;
	private Element enemyElement;
	public double currPlayerHealth;
	public double currEnemyHealth;
	public bool playerAdvantage;
	public bool enemyAdvantage;
	private string playerMoveName;
	private string enemyMoveName;
	private AnimatedSprite playerSprite;
	private AnimatedSprite enemySprite;
	public EnemyWizard enemyWizard;
	private ElementalMove enemyProjectile;
	private ElementalMove playerProjectile;
	private bool firstChoice = true;
	public enum EAction
	{
		ATTACK,
		BLOCK,
		HEAL
	}
	public Element PlayerElement
	{
		get { return playerElement; }
		set { playerElement = value; }
	}
	public Element EnemyElement
	{
		get { return enemyElement; }
		set { enemyElement = value; }
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

	public void DealDamageAfterHit(bool playerHit = false)
	{
		if (playerHit)
		{
			double damage = CalculateDamage(enemyWizard.Wisdom, enemyAdvantage, playerBlocking);
			UserInterface.Active.AddEntity(new Notification($"The enemy did {damage} damage to you!"));
			currPlayerHealth -= damage;
			battleScreen.UnlockActionButtons();
		}
		else
		{
			double damage = CalculateDamage(Core.playerWizard.Wisdom, playerAdvantage, enemyBlocking);
			UserInterface.Active.AddEntity(new Notification($"You did {damage} damage to the enemy!"));
			currEnemyHealth -= damage;
			battleScreen.LockActionButtons();
		}
		battleScreen.UpdateHealthBars();

	}
	public BlockType BlockChance()
	{


		return BlockType.GLANCING_BLOW;
	}
	public double CalculateDamage(byte wisdom, bool hasAdvantage, bool hasBlocked)
	{
		double damage = wisdom;
		if (hasAdvantage)
		{

			damage *= 1.5;
		}

		if (hasBlocked)
		{
			BlockType blockType = BlockChance();
			switch (blockType)
			{
				case BlockType.GLANCING_BLOW:
					damage /= 2;
					break;
				case BlockType.COMPLETE:
					damage = 0;
					break;
			}
		}

		return damage;
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
			new Vector2((float)(Core.screenWidth * 0.8), (float)(Core.screenHeight * 0.5)),
			SpriteEffects.FlipHorizontally
		));

	}

	public void PromptElementSelection()
	{
		// Creates the dropdown and adds elements as the dropdown items
		DropDown elementDropdown = new();
		// Get the elements from the Element Enum
		Element[] elements = Enum.GetValues<Element>();
		foreach (var element in elements)
		{
			elementDropdown.AddItem(element.ToString());
		};


		Button confirmElement = new("")
		{
			Locked = true,
			OnClick = (e) =>
			{

			}
		};
		MessageBox.ShowMsgBox("Choose Your Element", "Choose the element you want to use as your next move", new MessageBox.MsgBoxOption[] {
			new MessageBox.MsgBoxOption("Confirm Element", () => {
				if (elementDropdown.SelectedValue == null) return false;
				Core.battleHandler.PlayerElement = elements.Where(e => e.ToString() == elementDropdown.SelectedValue).First();
				Core.battleHandler.EnemyElement = elements[_Utils.GenerateRandomInteger(elements.Length)];
				if (firstChoice) {
					Core.battleHandler.StartBattle();
					firstChoice = false;
				}
				return true;
				})
		}, new Entity[] {
					elementDropdown
					});
	}
	public void BattleInit()
	{
		Core.exitGame.Visible = false;
		enemyWizard = Core.wizardHandler.CreateEnemyWizard();
		currPlayerHealth = Core.playerWizard.MaxHealth;
		currEnemyHealth = enemyWizard.MaxHealth;
		if (enemyWizard.Dexterity > Core.playerWizard.Dexterity)
		{
			enemyCasting = true;
		}
		else if (Core.playerWizard.Dexterity < enemyWizard.Dexterity)
		{
			playerCasting = true;
			PromptElementSelection();
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
					PromptElementSelection();
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
		
	}

	public void StartBattle()
	{
		// Set the move name for the player's element
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
		// Set the move name for the enemy's element
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
		// Calls a function to create elemental sprites
		CreateSprites();
		// Store the move that the player needs for their current element, this is changed when their move is over
		playerProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			playerSprite.Center,
			enemySprite.Position,
			eDirection.Right,
			$"PlayerProjectile_{playerMoveName.Replace(" ", "_")}"
		);
		// Store the move that the enemy needs for their current element, this is changed when their move is over
		enemyProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			enemySprite.Center,
			playerSprite.Position,
			eDirection.Left,
			$"EnemyProjectile_{enemyMoveName.Replace(" ", "_")}"
		);
		// Displays the BattleScreen
		battleScreen = new BattleScreen();
		UserInterface.Active.AddEntity(battleScreen);
		MessageBox.ShowYesNoMsgBox("Start Battle", "Do you wish to start the battle?", () =>
		{
			battleScreen.InitBattleScreen(playerCasting);
			return true;
		},
		() =>
		{
			return false;
		});
	}

	public void CreateProjectiles()
	{
		// A fight is active, check who is attacking, then send their projectile
		if (playerCasting)
		{
			//Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "CastSpell";
			playerProjectile = Core.projectileHandler.NewElementalMove(playerProjectileData, playerMoveName);
			playerProjectile.Fire();
			Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "Idle";
			playerCasting = false;
		}

		if (enemyCasting)
		{
			Console.WriteLine("OMG THE ENEMY IS ATTACKING");
			//Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "CastSpell";
			enemyProjectile = Core.projectileHandler.NewElementalMove(enemyProjectileData, enemyMoveName);
			enemyProjectile.Fire();
			Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "Idle";
			enemyCasting = false;
		}

	}

	public void HandleAttack(bool initAttack = false)
	{

		if ((currPlayerHealth <= 0 || currEnemyHealth <= 0) && !initAttack)
		{
			battleScreen?.LockActionButtons();
			if (currPlayerHealth <= 0)
			{
				currPlayerHealth = 0;
				playerSprite.CurrentAnimation = "Death";
				Console.WriteLine("PLAYER DEAD!");
			}
			else if (currEnemyHealth <= 0)
			{
				currEnemyHealth = 0;
				enemySprite.CurrentAnimation = "Death";
				Console.WriteLine("ENEMY DEAD!");
			}
			battleScreen.DestroyBattleScreen();
			UserInterface.Active.AddEntity(new GameResult(currPlayerHealth == 0, Core.playerWizard));
			return;
		}


		if (playerCasting)
		{
			// Create projectiles before toggling the casting flags
			CreateProjectiles();
			playerCasting = false;
			enemyCasting = true;
			battleScreen.LockActionButtons();
		}
		if (enemyCasting)
		{
			CreateProjectiles();
			playerCasting = true;
			enemyCasting = false;
			battleScreen.UnlockActionButtons();
		}
		

	}
	public void HandleBlock(bool playerWantsToBlock = false)
	{
		if (playerWantsToBlock)
		{
			playerBlocking = true;
		}
		else
		{
			enemyBlocking = true;
		}
	}
}