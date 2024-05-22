using GeonBit.UI;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using TheWiseOneQuest.Components;
using GeonBit.UI.Entities;
using TheWiseOneQuest.Models;
using System.Collections.Generic;
namespace TheWiseOneQuest.Screens;

public class BattleScreen : Menu
{
    List<Button> actionButtons = new();
    public void LockActionButtons()
    {
        foreach (var button in actionButtons)
        {
            button.Locked = true;
        }
    }
    public BattleScreen()
    {
        Scale = 1f;
        _indexInParent = -1;
        Opacity = 1;
        Size = new Vector2(0, 0);

        // Core.battleHandler.StartBattle();
        WizardInfo playerInfo = new WizardInfo(Core.playerWizard)
        {
            Anchor = Anchor.TopLeft
        };

        Button attackButton = new("Attack!");
        Button blockButton = new("Block!");
        Button healButton = new("Heal Yourself!");

        actionButtons.Add(attackButton);
        actionButtons.Add(blockButton);
        actionButtons.Add(healButton);


        if (!Core.battleHandler.PlayerTurn)
        {
            // action buttons are locked
            LockActionButtons();
        }

        AddChild(playerInfo);
        Button returnToMenu = new("Main Menu", ButtonSkin.Default, Anchor.BottomCenter, size: new Vector2(0.25f, 0.05f))
        {
            OnClick = (Entity bt) =>
            {
                Core.exitGame.Visible = true;
                Core.spriteHandler.ClearAnimatedSprites();
                UserInterface.Active.AddEntity(new MainMenu());
                RemoveFromParent();
            }
        };
        AddChild(returnToMenu);
    }
}
