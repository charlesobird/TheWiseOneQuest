using System;
using System.Collections.Generic;
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
using TheWiseOneQuest.Models.Sprites;
using TheWiseOneQuest.Screens;
using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest
{
    public class TheWiseOneQuest : Game
	{
		public static ContentManager _Content;
		public static GraphicsDeviceManager graphics;

		public static WizardHandler wizardHandler;
		SpriteBatch spriteBatch;
		public static int screenWidth;
		public static int screenHeight;
		public static Panel mainMenu;
		public static Button exitGame;
		static PlaySelection playSelection;
		static AnimatedSprite sprite;
		public static Texture2D ActualTexture;
		public static Dictionary<string, Animation> animations;

		public static PlayerWizard playerWizard = null;

		public TheWiseOneQuest()
		{
			wizardHandler = new WizardHandler();
			wizardHandler.FindWizardStore();
			Services.AddService(typeof(WizardHandler), wizardHandler);
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

            UserInterface.Active.AddEntity(new MainMenu());

			exitGame = new Button(
				"Exit Game",
				ButtonSkin.Default,
				Anchor.BottomCenter,
				new Vector2(0.2f, -1)
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
			animations = new();

			Animation wizDeathAnim = new(6, 128, 128, 0, 128, false) { CurrentFrame = 0, FramesPerSecond = 8 };
			animations.Add("Death", wizDeathAnim);
			Animation wizIdleAnim = new(7, 128, 128, 0, 0, true) { CurrentFrame = 0, FramesPerSecond = 8 };
			animations.Add("Idle", wizIdleAnim);


		}

		public static void ShowBattleScreen()
		{
			ActualTexture = _Content.Load<Texture2D>("Animations/AirWizard");
			sprite = new(ActualTexture, animations, 256,256)
			{
				CurrentAnimation = "Idle",
				IsActive = true,
				IsAnimating = true
			};
			UserInterface.Active.AddEntity(new BattleScreen());
			exitGame.Visible = false;
		}

		protected override void Update(GameTime gameTime)
		{
			// GeonBit.UIL update UI manager
			UserInterface.Active.Update(gameTime);
			// if (
			// 	GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
			// 	|| Keyboard.GetState().IsKeyDown(Keys.Escape)
			// )
			// {
			// 	if (pauseMenu == null)
			// 	{
			// 		pauseMenu = new PauseMenu();
			// 		UserInterface.Active.AddEntity(pauseMenu);

			// 		pauseMenu = null;
			// 	}
			// }

			// tbd add your own update() stuff here..
			// if (Keyboard.GetState().GetPressedKeys().Contains(Keys.F12))
			// {
			// 	_Utils.ToggleFullscreen();
			// }
			// call base update
			base.Update(gameTime);
			if (sprite != null)
			{
				sprite.Update(gameTime);
			}
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
			if (sprite != null) {
				sprite.Position = new Vector2(0,0.25f);
				sprite.Draw(spriteBatch);
			}
			spriteBatch.End();

			UserInterface.Active.DrawMainRenderTarget(spriteBatch);
			// call base draw function
			base.Draw(gameTime);
		}
	}
}
