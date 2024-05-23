using Core = TheWiseOneQuest.TheWiseOneQuest;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Models;
using GeonBit.UI;
using GeonBit.UI.Utils;
namespace TheWiseOneQuest.Screens;

public class GameResult : Panel
{
    public void RestartCharacter(PlayerWizard playerWizard)
    {
        RemoveFromParent();
        Core.SetPlayerWizard(playerWizard);
        Core.StartBattleHandler();
    }
    public void NewCharacter()
    {
        RemoveFromParent();
        UserInterface.Active.AddEntity(new CharacterCreation());
    }
    public GameResult(bool gameWon, PlayerWizard playerWizard)
    {
        Size = new Vector2(0.5f);
        Anchor = Anchor.Center;
        string resultString = gameWon ? $"Congratulations {playerWizard.Name}" : $"Do not despair {playerWizard.Name}";
        Header resultText = new(resultString) { FillColor = gameWon ? Color.LightGreen : Color.Red };
        string btnText = gameWon ? "Start a New Character" : $"Try Again as {playerWizard.Name}";
        string supportiveText = gameWon ? "You have won the tournament, I declare you as The Wise One, you have proved yourself to be the most powerful Wizard" : "You may have lost this one, but there will always be more for you out there to prove yourself as The Wise One.";
        Paragraph supportiveParagraph = new(supportiveText, Anchor.AutoCenter, new Vector2(0.5f,-1));
        Button characterChoiceButton = new(btnText)
        {
            Size = new Vector2(0.5f, -1),
            OnClick = (e) =>
            {
                if (gameWon)
                {
                    NewCharacter();
                }
                else
                {
                    RestartCharacter(playerWizard);
                }
            },
            Anchor = Anchor.Center
        };
        Button mainMenu = new("Main Menu")
        {
            Size = new Vector2(-1),
            OnClick = (e) =>
            {
                Core.exitGame.Visible = true;
                UserInterface.Active.AddEntity(new MainMenu());
                RemoveFromParent();
            },
        };
        Button exitGame = new("Exit Game")
        {
            Size = new Vector2(-1),
            OnClick = (e) =>
            {
                Core.exitGame.OnClick.Invoke(e);
            },
        };
        Panel bottomPanel = new(
            Size = new Vector2(0.5f),
            PanelSkin.None,
            Anchor.AutoCenter
        );
        var columnPanels = PanelsGrid.GenerateColums(2, bottomPanel);
        foreach (var column in columnPanels)
        {
            column.Padding = Vector2.Zero;
        }

        Panel leftPanel = columnPanels[0];
        Panel rightPanel = columnPanels[1];
        leftPanel.AddChild(mainMenu);
        rightPanel.AddChild(exitGame);
        AddChild(resultText);
        AddChild(supportiveParagraph);
        AddChild(characterChoiceButton);
        AddChild(bottomPanel);
    }
}