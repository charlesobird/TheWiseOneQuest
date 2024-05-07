using TheWiseOneQuest.Components;
using Core = TheWiseOneQuest.TheWiseOneQuest;

namespace TheWiseOneQuest.Screens;

public class PauseMenu : Menu
{
	public PauseMenu()
	{
		Size = new Microsoft.Xna.Framework.Vector2(
			TheWiseOneQuest.screenWidth,
			TheWiseOneQuest.screenHeight
		);
		AddReturnButton();
		// UserInterface.Active.Clear();
	}
}
