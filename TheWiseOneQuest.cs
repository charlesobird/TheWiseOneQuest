using System;
using System.Collections.Generic;
using System.Linq;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Handlers;
using TheWiseOneQuest.Models;
using TheWiseOneQuest.Models.Sprites;
using TheWiseOneQuest.Screens;
using TheWiseOneQuest.Utils;
using _Utils = TheWiseOneQuest.Utils.Utils;
using _WizardSpriteLocations = TheWiseOneQuest.Utils.WIZARD_SPRITE_SHEET_LOCATIONS;

namespace TheWiseOneQuest
{
    public class TheWiseOneQuest : Game
    {
        public static ContentManager _Content;
        public static GraphicsDeviceManager graphics;

        public static dynamic[] activeSprites = Array.Empty<dynamic>();

        public static WizardHandler wizardHandler;
        SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;
        public static Panel mainMenu;
        public static Button exitGame;
        public static AnimatedSprite playerSprite;
        public static AnimatedSprite enemySprite;
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

            Animation wizIdleAnim =
                new(7, 128, 128, 0, (int)_WizardSpriteLocations.Idle, true)
                {
                    CurrentFrame = 0,
                    FramesPerSecond = 3
                };
            animations.Add("Idle", wizIdleAnim);
            Animation wizDeathAnim =
                new(6, 128, 128, 0, (int)_WizardSpriteLocations.Death, false)
                {
                    CurrentFrame = 0,
                    FramesPerSecond = 5
                };
            animations.Add("Death", wizDeathAnim);
        }

        public static void CreateBattleSprites(Element playerElement, Element enemyElement)
        {
            Texture2D playerSpriteSheet = _Content.Load<Texture2D>(
                GetSpriteSheetForElement(playerElement)
            );
            Texture2D enemySpriteSheet = _Content.Load<Texture2D>(
                GetSpriteSheetForElement(enemyElement)
            );

            playerSprite = new(playerSpriteSheet, animations, 256, 256)
            {
                CurrentAnimation = "Idle",
                IsActive = true,
                IsAnimating = true
            };
            enemySprite = new(enemySpriteSheet, animations, 256, 256)
            {
                CurrentAnimation = "Idle",
                IsActive = true,
                IsAnimating = true
            };
            // playerSprite.Position = new Vector2(0,0);
            // enemySprite.Position = new Vector2(256, 0);
            activeSprites.Append(playerSprite);
            activeSprites.Append(enemySprite);
        }

        public static void ShowBattleScreen()
        {
            UserInterface.Active.AddEntity(new BattleScreen());
            exitGame.Visible = false;
            CreateBattleSprites(Element.Air, Element.Air);
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

        public static void ResetSprites()
        {
            Array.Clear(activeSprites);
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
            playerSprite?.Update(gameTime);
            enemySprite?.Update(gameTime);
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
            playerSprite?.Draw(spriteBatch);
            enemySprite?.Draw(spriteBatch);
            spriteBatch.End();

            UserInterface.Active.DrawMainRenderTarget(spriteBatch);
            // call base draw function
            base.Draw(gameTime);
        }
    }
}
