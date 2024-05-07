using GeonBit.UI.Entities;
using GeonBit.UI.Entities.TextValidators;
using TheWiseOneQuest.Components;
using TheWiseOneQuest.Models;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TheWiseOneQuest.Screens;

public class CharacterCreation : Menu
{
	public CharacterCreation()
	{
		AddChild(new Label(@"Name: ", Anchor.AutoInline, size: new Vector2(0.4f, -1)));
		TextInput nameInput = new TextInput(false, new Vector2(0.4f, -1), anchor: Anchor.AutoInline)
		{
			PlaceholderText = "Name"
		};
		nameInput.Validators.Add(new EnglishCharactersOnly(true));
		nameInput.Validators.Add(new OnlySingleSpaces());
		nameInput.Validators.Add(new MakeTitleCase());
		AddChild(nameInput);

		AddReturnButton();
	}
}
