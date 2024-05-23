global using Core = TheWiseOneQuest.TheWiseOneQuest;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;

namespace TheWiseOneQuest.Screens;

public class FightEnd : Menu
{
    public FightEnd(bool fightWon)
    {
        Header header = new("The Fight is Over");
        Paragraph paragraph = new("Come back tomorrow to fight again!");
        Paragraph feedbackParagraph = new();
        if (fightWon)
        {
            feedbackParagraph.Text = "Congratulations! You are one step closer to becoming \"The Wise One\"";
        }
        else
        {
            feedbackParagraph.Text = "Don't worry, come back tomorrow and try again and be one step closer to becoming \"The Wise One\"";
        }

        Button changeCharacter = new("Change Character");
        Button exitGame = new("Exit Game")
        {
            OnClick = (e) =>
            {
                Core.exitGame.OnClick.Invoke(e);
            },
        };
    }
}