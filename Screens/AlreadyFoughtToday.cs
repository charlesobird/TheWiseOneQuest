namespace TheWiseOneQuest.Screens;

public class AlreadyFoughtToday : Panel
{
    public AlreadyFoughtToday()
    {
        Header header = new("Unable to Fight");

        Paragraph paragraph = new("You've already fought an opponent today, come back tomorrow or choose a different character.");

        Button changeCharacter = new("Change Character")
        {
            OnClick = (e) =>
            {   
                CharacterSelection characterSelection = new();
                UserInterface.Active.AddEntity(characterSelection);
                characterSelection.SetupUI(Core.wizardHandler.GetWizards());
                RemoveFromParent();
            },
            Anchor = Anchor.AutoCenter
        };
        Button exitGame = new("Exit Game")
        {
            OnClick = Core.exitGame.OnClick.Invoke,
        };
        AddChild(header);
        AddChild(paragraph);
        AddChild(changeCharacter);
        AddChild(exitGame);
    }
}