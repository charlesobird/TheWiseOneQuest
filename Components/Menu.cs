using GeonBit.UI.Entities;

namespace TheWiseOneQuest.Components;

public class Menu : Panel
{
	public Menu()
	{ 
		Scale = 1f;
		Size = new Microsoft.Xna.Framework.Vector2(-1,-1);
	}
	public void AddReturnButton()
	{
		Button returnToMenu = new Button(
				"Return",
				ButtonSkin.Default,
							Anchor.BottomCenter
			);
		returnToMenu.OnClick = (Entity bt) =>
		{
			RemoveFromParent();
		};
		AddChild(returnToMenu);
	}
}