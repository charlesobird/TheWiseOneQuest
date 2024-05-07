using System;
using System.Linq;
using System.Threading;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheWiseOneQuest.Handlers;
using TheWiseOneQuest.Models;
using TheWiseOneQuest.Screens;
using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest
{
	public class TheWiseOneQuest : Game
	{
		public static ContentManager _Content;
		public static GraphicsDeviceManager graphics;

		private WizardHandler _wizardHandler;
		SpriteBatch spriteBatch;
		public static int screenWidth;
		public static int screenHeight;
		public static Panel mainMenu;
		public static Button exitGame;
		static PlaySelection playSelection;

		public AnimationHandler animatedSprite = new AnimationHandler("Example/mage");
		public static PlayerWizard playerWizard = null;

		public TheWiseOneQuest()
		{
			_wizardHandler = new WizardHandler();
			_wizardHandler.FindWizardStore();
			Services.AddService(typeof(WizardHandler), _wizardHandler);
			graphics = new GraphicsDeviceManager(this)
			{
				HardwareModeSwitch = false // stops widescreen look
			};
			graphics.ToggleFullScreen(); // force full screen
			Content.RootDirectory = "Content";
			_Content = Content;
			int _ScreenWidth = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
			int _ScreenHeight = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
			Window.AllowUserResizing = true;

			screenWidth = _ScreenWidth;
			screenHeight = _ScreenHeight;
		}

		protected override void Initialize()
		{
			UserInterface.Initialize(Content, BuiltinThemes.hd);
			UserInterface.Active.UseRenderTarget = true;

			UserInterface.Active.IncludeCursorInRenderTarget = false;

			mainMenu = new(new Vector2(550, -1), PanelSkin.Default, Anchor.Center)
			{
				OutlineColor = Color.PaleVioletRed,
				Scale = 1f
			};
			UserInterface.Active.AddEntity(mainMenu);
			Header header = new Header("The Wise One's Quest") { FillColor = Color.White };
			mainMenu.AddChild(header);
			mainMenu.AddChild(new HorizontalLine());

			Button playButton = new Button("Play");
			Button settingsButton = new Button("Settings");
			playButton.OnClick = (Entity e) =>
			{
				playSelection = new PlaySelection();
				UserInterface.Active.AddEntity(playSelection);
			};
			settingsButton.OnClick = (Entity e) =>
			{
				UserInterface.Active.AddEntity(new SettingsMenu(graphics));
			};

			mainMenu.AddChild(playButton);
			mainMenu.AddChild(settingsButton);

			exitGame = new Button(
				"Exit Game",
				ButtonSkin.Default,
				Anchor.BottomCenter,
				new Vector2(_Utils.GetPercentageOfScreenWidth(0.2), 50)
			)
			{
				Offset = new Vector2(15),
				OnClick = (Entity e) =>
				{
					Exit();
				}
			};
			UserInterface.Active.AddEntity(exitGame);

			base.Initialize();
		}

		public static void SetPlayerWizard(PlayerWizard selectedWizard)
		{
			playerWizard = selectedWizard;
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		public static void ShowBattleScreen()
		{
			UserInterface.Active.AddEntity(new BattleScreen(playerWizard, _Content));
			mainMenu.Visible = false;
			exitGame.Visible = false;
			playSelection.Visible = false;
		}

		/// Allows the game to run logic such as updating the world.
		/// here we call the UI manager update() function to update the UI. <summary>
		/// Allows the game to run logic such as updating the world.
		/// </summary>
		/// <param name="gameTime"></param>
		public static bool paused = false;
		public static int lastTick = 0;
		public static PauseMenu pauseMenu = null;

		protected override void Update(GameTime gameTime)
		{
			// Check if the timer has exceeded the threshold.
			if (animatedSprite.timer > animatedSprite.threshold)
			{
				// If Alex is in the middle sprite of the animation.
				if (animatedSprite.currentAnimationIndex == 1)
				{
					// If the previous animation was the left-side sprite, then the next animation should be the right-side sprite.
					if (animatedSprite.previousAnimationIndex == 0)
					{
						animatedSprite.currentAnimationIndex = 2;
					}
					else

					// If not, then the next animation should be the left-side sprite.
					{
						animatedSprite.currentAnimationIndex = 0;
					}

					// Track the animation.
					animatedSprite.previousAnimationIndex = animatedSprite.currentAnimationIndex;
				}
				// If Alex was not in the middle sprite of the animation, he should return to the middle sprite.
				else
				{
					animatedSprite.currentAnimationIndex = 1;
				}

				// Reset the timer.
				animatedSprite.timer = 0;
			}
			// If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
			else
			{
				animatedSprite.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			}
			// GeonBit.UIL update UI manager
			UserInterface.Active.Update(gameTime);
			if (
				GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				|| Keyboard.GetState().IsKeyDown(Keys.Escape)
			)
			{
				if (pauseMenu == null)
				{
					pauseMenu = new PauseMenu();
					UserInterface.Active.AddEntity(pauseMenu);

					pauseMenu = null;
				}
			}

			// tbd add your own update() stuff here..
			if (Keyboard.GetState().GetPressedKeys().Contains(Keys.F11) && !paused)
			{
				_Utils.ToggleFullscreen();
				paused = true;
				Thread.Sleep(500);
				paused = false;
			}
			// call base update
			base.Update(gameTime);
		}

		/// This is called when the game should draw itself.
		/// here we call the UI manager draw() function to render the UI.
		protected override void Draw(GameTime gameTime)
		{
			// clear buffer
			GraphicsDevice.Clear(Color.CornflowerBlue);

			UserInterface.Active.Draw(spriteBatch);
			spriteBatch.Begin();
			spriteBatch.Draw(Content.Load<Texture2D>("background"), new Vector2(0, 0), Color.White);
			//spriteBatch.Draw(Content.Load<Texture2D>(animatedSprite.SpriteTexture2DName), new Vector2(100,100), color:Color.White);
			spriteBatch.Draw(Content.Load<Texture2D>(animatedSprite.SpriteTexture2DName), position: new Vector2(100, 100), sourceRectangle: animatedSprite.sourceRectangles[animatedSprite.currentAnimationIndex], Color.White);
			spriteBatch.End();
			// spriteBatch.Begin();
			// spriteBatch.Draw(Content.Load<Texture2D>("Example/mage"), new Vector2(0, 0), Color.White);
			// spriteBatch.End();
			// UserInterface.Active.UseRenderTarget = true;

			UserInterface.Active.DrawMainRenderTarget(spriteBatch);
			// call base draw function
			base.Draw(gameTime);
		}
	}
}
