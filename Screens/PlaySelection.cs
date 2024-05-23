namespace TheWiseOneQuest.Screens;

public class PlaySelection : Menu
{
	public PlaySelection()
	{
		_anchor = Anchor.Center;
		Dictionary<string, PlayerWizard> wizards = Core.wizardHandler.GetWizards();
		Header header = new Header("Selection") { FillColor = Color.White };
		AddChild(header);
		AddChild(new HorizontalLine());

		Button chooseCharacter = new("Choose Your Character");
		if (ReferenceEquals(wizards, null) || wizards.Count == 0)
		{
			chooseCharacter.Visible = false;
		}
		Button createCharacter = new("Create A New Character");
		chooseCharacter.OnClick = (Entity e) =>
		{
            RemoveFromParent();
			CharacterSelection characterSelection = new CharacterSelection();
			UserInterface.Active.AddEntity(characterSelection);
			characterSelection.SetupUI(wizards);
		};
		createCharacter.OnClick = (Entity e) =>
		{
            RemoveFromParent();
			CharacterCreation characterCreation = new CharacterCreation();
			UserInterface.Active.AddEntity(characterCreation);
		};
		AddChild(chooseCharacter);
		AddChild(createCharacter);
		AddReturnButton((Entity e) => {
            UserInterface.Active.AddEntity(new MainMenu());
        });
	}
}
