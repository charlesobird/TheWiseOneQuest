using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Models;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using TheWiseOneQuest.Handlers;

namespace TheWiseOneQuest.Screens;

public class BattleScreen : Panel
{
	public BattleScreen(PlayerWizard playerWizard, ContentManager content)
	{
		Scale = 1f;
		_indexInParent = -1;
		Opacity = 1;
		Size = new Vector2(Core.screenWidth, Core.screenHeight);
		
		AddChild(new Image(anchor: Anchor.CenterLeft, texture: content.Load<Texture2D>("Example/mage"), size: _Utils.WIZARD_SPRITE_SIZE));

		Button returnToMenu = new Button(
				"To Main Menu",
				ButtonSkin.Default,
							Anchor.BottomCenter
			);
		returnToMenu.OnClick = (Entity bt) =>
		{
			Core.mainMenu.Visible = true;
			Core.exitGame.Visible = true;
			RemoveFromParent();
		};
		AddChild(returnToMenu);
	}
}
