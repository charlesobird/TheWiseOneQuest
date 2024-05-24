using System.Data;

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
	private bool attackInProgress = false;
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
	private bool gameOver = false;

	public BattleHandler() { }

	public void DealDamageAfterHit(bool playerHit = false)
	{
		if (playerHit)
		{
			double damage = CalculateDamage(enemyWizard.Wisdom, enemyAdvantage, playerBlocking);
			battleScreen.playerInfo.infoParagraph.Text = $"You took {damage} damage!";
			currPlayerHealth -= damage;
			playerBlocking = false;
			enemyCasting = false;
			playerCasting = true;
		}
		else
		{
			double damage = CalculateDamage(Core.playerWizard.Wisdom, playerAdvantage, enemyBlocking);
			battleScreen.enemyInfo.infoParagraph.Text = $"The enemy took {damage} damage!";
			currEnemyHealth -= damage;
			enemyBlocking = false;
			playerCasting = false;
			enemyCasting = true;
		}
		CheckForDeadWizard();
		battleScreen.UpdateHealthBars();

	}
	public BlockType BlockChance()
	{

		double breakChance = _Utils.GenerateRandomDouble();
		if (breakChance <= _Utils.PERFECT_BLOCK_PERCENTAGE_CHANCE)
		{
			return BlockType.COMPLETE;
		}
		else if (breakChance > _Utils.PERFECT_BLOCK_PERCENTAGE_CHANCE)
		{
			return BlockType.GLANCING_BLOW;
		}
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
			new Vector2((float)(Core.screenWidth * 0.8), (float)(Core.screenHeight * 0.6)),
			SpriteEffects.FlipHorizontally
		));

	}

	public void PromptElementSelection()
	{
		if (gameOver) {
			return;
		}
		if (playerSprite != null && enemySprite != null)
		{
			playerSprite.CurrentAnimation = "Idle";
			enemySprite.CurrentAnimation = "Idle";
		}
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
				playerElement = elements.Where(e => e.ToString() == elementDropdown.SelectedValue).First();
				enemyElement = elements[_Utils.GenerateRandomInteger(maxValue:elements.Length)];
				if (firstChoice) {
					StartBattle();
					firstChoice = false;
				} else {
					Core.spriteHandler.ClearAnimatedSprites();
					GenerateProjectileDataAndSprites();
				}
				if (playerAdvantage)
				{
					battleScreen.playerInfo.infoParagraph.Text = "You have a 50% damage bonus this attack!";
				}
				if (enemyAdvantage)
				{
					battleScreen.enemyInfo.infoParagraph.Text = "The enemy has a 50% damage bonus this attack!";
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
	public void GenerateProjectileDataAndSprites()
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
		playerAdvantage = Core.playerWizard.CheckIfBuffActive(enemyElement, playerElement);
		enemyAdvantage = enemyWizard.CheckIfBuffActive(playerElement, enemyElement);
		// Calls a function to create elemental sprites
		CreateSprites();
		// Store the move that the player needs for their current element, this is changed when their move is over
		playerProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].Center,
			Core.spriteHandler.activeAnimatedSprites["EnemySprite"].Position,
			eDirection.Right,
			$"PlayerProjectile_{playerMoveName.Replace(" ", "_")}"
		);
		// Store the move that the enemy needs for their current element, this is changed when their move is over
		enemyProjectileData = new(
			_Utils.Content.Load<Texture2D>($"Sprites/Projectiles/ElementalProjectiles"),
			Core.projectileHandler.projectileAnimations,
			_Utils.DEFAULT_PROJECTILE_SIZE,
			Core.spriteHandler.activeAnimatedSprites["EnemySprite"].Center,
			Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].Position,
			eDirection.Left,
			$"EnemyProjectile_{enemyMoveName.Replace(" ", "_")}"
		);
	}
	public void StartBattle()
	{
		GenerateProjectileDataAndSprites();
		// Displays the BattleScreen
		battleScreen = new BattleScreen();
		UserInterface.Active.AddEntity(battleScreen);
		MessageBox.ShowYesNoMsgBox("Start Battle", "Do you wish to start the battle?", () =>
		{
			Task.Run(async () =>
			{
				await battleScreen.InitBattleScreen(playerCasting);
			});
			return true;
		},
		() =>
		{
			return false;
		});
	}
	public void CreatePlayerProjectile()
	{
		playerProjectile = Core.projectileHandler.NewElementalMove(playerProjectileData, playerMoveName);
		playerProjectile.Fire();
		playerSprite.CurrentAnimation = "Idle";
		//Core.spriteHandler.activeAnimatedSprites["PlayerSprite"].CurrentAnimation = "Idle";
	}
	public void CreateEnemyProjectile()
	{
		enemyProjectile = Core.projectileHandler.NewElementalMove(enemyProjectileData, enemyMoveName);
		enemyProjectile.Fire();
		enemySprite.CurrentAnimation = "Idle";
		//Core.spriteHandler.activeAnimatedSprites["EnemySprite"].CurrentAnimation = "Idle";
	}
	public void PlayerAttack()
	{
		if (!gameOver)
		{
			CreatePlayerProjectile();
		}
	}
	public void EnemyAttack()
	{
		if (!gameOver)
		{
			CreateEnemyProjectile();
		}

	}
	public void CheckForDeadWizard()
	{
		if (currPlayerHealth <= 0 || currEnemyHealth <= 0)
		{
			gameOver = true;
			Core.playerWizard.RoundsPlayed++;
			// Check who has died
			if (currPlayerHealth <= 0)
			{
				currPlayerHealth = 0;
			}
			if (currEnemyHealth <= 0)
			{
				Core.playerWizard.RoundsWon++;
				currEnemyHealth = 0;
			}
			// Reset/Clear remaining Sprites
			Core.spriteHandler.ClearAnimatedSprites();
			Core.projectileHandler.ClearElementalMoves();
			battleScreen.DestroyBattleScreen();
			// Check if the player has won the tournament, if they have, show end of the game
			// if they haven't, show fight result + reminder to play again tomorrow
			dynamic gui;
			if (Core.playerWizard.RoundsWon == _Utils.DEFAULT_ROUNDS_WON_THRESHOLD)
			{
				Core.playerWizard.TheWiseOne = true;
				gui = new GameResult(true, Core.playerWizard); // Win the tournament
			}
			else
			{
				gui = new FightEnd(currPlayerHealth > 0, Core.playerWizard); // Come back tomorrow
			}
			// They got defeated at all rounds so automatic loss
			if (Core.playerWizard.RoundsPlayed == _Utils.DEFAULT_ROUNDS_WON_THRESHOLD)
			{
				gui = new GameResult(false, Core.playerWizard); // Lose the tournament
			}
			// Save new stats to the wizards.json file
			Core.wizardHandler.SaveWizardState(Core.playerWizard.Name, Core.playerWizard);
			UserInterface.Active.AddEntity(gui);
			return;
		}

	}
	public async Task AttackAsync()
	{
		// Make sure there aren't any dead wizards
		CheckForDeadWizard();
		// No ongoing attack
		if (attackInProgress)
		{
			attackInProgress = false;
			return;
		}
		attackInProgress = true;
		// Player attacks first
		if (playerCasting && !gameOver)
		{
			PlayerAttack();
			await Task.Delay(5000);  // This is so that the projectile has time to hit the other sprite
			CheckForDeadWizard();
			EnemyAttack();
			await Task.Delay(5000);  // This is so that the projectile has time to hit the other sprite
			CheckForDeadWizard();
		}
		// Enemy Attacks first
		else if (enemyCasting && !gameOver)
		{
			EnemyAttack();
			await Task.Delay(5000);  // This is so that the projectile has time to hit the other sprite
			CheckForDeadWizard();
			PlayerAttack();
			await Task.Delay(5000);  // This is so that the projectile has time to hit the other sprite
			CheckForDeadWizard();
		}
		if (gameOver) return;
		attackInProgress = false;
		// Next elemental move selection
		PromptElementSelection();
		// Make sure the user can select Attack, Block or Heal
		battleScreen.UnlockActionButtons();

	}
	public async void HandleBlock(bool playerWantsToBlock = false)
	{
		if (playerWantsToBlock)
		{
			playerBlocking = true;
			battleScreen.playerInfo.infoParagraph.Text = "\nYou will block the next attack";
			enemyCasting = true;
			playerCasting = false;
			EnemyAttack();
			await Task.Delay(5000);
			CheckForDeadWizard();
			PromptElementSelection();
		}
		else
		{
			enemyBlocking = true;
			battleScreen.enemyInfo.infoParagraph.Text = "\nThe enemy will block the next attack";
			playerCasting = true;
			enemyCasting = false;
			PlayerAttack();
		}
		battleScreen.UnlockActionButtons();
	}
	public async void HandleHeal(bool playerWantsToHeal = false)
	{
		battleScreen.LockActionButtons();
		if (playerWantsToHeal)
		{
			int healthToGive = _Utils.GenerateRandomInteger(1, Core.playerWizard.MaxHealth);
			battleScreen.playerInfo.infoParagraph.Text = $"\nYou got healed by {healthToGive}";
			currPlayerHealth += healthToGive;
			battleScreen.UpdateHealthBars();
			enemyCasting = true;
			playerCasting = false;
			EnemyAttack();
			await Task.Delay(5000);
			CheckForDeadWizard();
			PromptElementSelection();
		}
		else
		{
			int healthToGive = _Utils.GenerateRandomInteger(1, enemyWizard.MaxHealth);
			battleScreen.playerInfo.infoParagraph.Text = $"\nThe enemy got healed by {healthToGive}";
			currEnemyHealth += healthToGive;
			battleScreen.UpdateHealthBars();
			playerCasting = true;
			enemyCasting = false;
			PlayerAttack();
		}
	}
}