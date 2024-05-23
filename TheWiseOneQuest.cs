global using System;
global using System.IO;
global using System.Linq;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using GeonBit.UI;
global using GeonBit.UI.Entities;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using TheWiseOneQuest.Handlers;
global using TheWiseOneQuest.Models;
global using TheWiseOneQuest.Models.Sprites;
global using TheWiseOneQuest.Screens;
global using TheWiseOneQuest.Utils;
global using _Utils = TheWiseOneQuest.Utils.Utils;
global using GeonBit.UI.Entities.TextValidators;
global using GeonBit.UI.Utils;
global using TheWiseOneQuest.Components;
global using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TheWiseOneQuest
{
    public class TheWiseOneQuest : Game
    {
        public static GraphicsDeviceManager graphics;

        public static List<ElementalMove> elementalProjectiles = new List<ElementalMove>();
        public static WizardHandler wizardHandler = new();
        public static BattleHandler battleHandler = new();
        public static ProjectileHandler projectileHandler = new();
        public static SpriteHandler spriteHandler = new();
        public static SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;
        public static Panel mainMenu;
        public static Button exitGame;
        public static AnimatedSprite playerSprite;
        public static AnimatedSprite enemySprite;
        public static Dictionary<string, Animation> animations;
        public static Dictionary<string, Animation> projectileAnimations;

        public static PlayerWizard playerWizard = null;

        public TheWiseOneQuest()
        {
            wizardHandler.FindWizardStore();
            // Services.AddService(typeof(WizardHandler), wizardHandler);
            graphics = new GraphicsDeviceManager(this)
            {
                HardwareModeSwitch = false // stops widescreen look
            };
            graphics.ToggleFullScreen(); // force full screen
            Content.RootDirectory = "Content";
            _Utils.Content = Content;
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

            spriteHandler.CreateWizardAnimations();
            projectileHandler.CreateProjectileAnimations();

        }

        public static void CreateBattleSprites(Element playerElement, Element enemyElement)
        {

            /* 			Texture2D elemProjectilesSpriteSheet = _Utils.Content.Load<Texture2D>(
                            "Sprites/Projectiles/ElementalProjectiles"
                        );

                        ElementalMove fireball = projectileHandler.NewElementalMove(new ProjectileData(
                            elemProjectilesSpriteSheet,
                            projectileAnimations,
                            _Utils.DEFAULT_PROJECTILE_SIZE,
                            new Vector2(playerSprite.Center.X + 64, playerSprite.Center.Y + 64),
                            enemySprite.Position,
                            eDirection.Right
                        ));
                        ElementalMove tornado = projectileHandler.NewElementalMove(new ProjectileData(
                            elemProjectilesSpriteSheet,
                            projectileAnimations,
                            _Utils.DEFAULT_PROJECTILE_SIZE,
                            new Vector2(playerSprite.Center.X + 64, playerSprite.Center.Y + 64),
                            enemySprite.Position,
                            eDirection.Left
                        ));
                        fireball.CurrentAnimation = "Fireball";
                        tornado.CurrentAnimation = "Tornado"; */
            // playerSprite.CurrentAnimation = "CastSpell";
            // projectileHandler.FireAllElementalMoves();
        }

        public static void StartBattleHandler()
        {
            battleHandler.BattleInit();
        }



        protected override void Update(GameTime gameTime)
        {
            // GeonBit.UIL update UI manager
            UserInterface.Active.Update(gameTime);
            spriteHandler.Update(gameTime);
            projectileHandler?.ClearFinishedElementalMoves();
            projectileHandler.Update(gameTime);
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
        }

        /// This is called when the game should draw itself.
        /// here we call the UI manager draw() function to render the UI.

        protected override void Draw(GameTime gameTime)
        {
            // clear buffer
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing
            UserInterface.Active.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("Summer7"), Vector2.One, Color.White);
            spriteHandler.Draw(spriteBatch);
            projectileHandler.Draw(spriteBatch);
            spriteBatch.End();

            // RenderTarget2D
            UserInterface.Active.DrawMainRenderTarget(spriteBatch);

            // call base draw function
            base.Draw(gameTime);
        }
    }
}
