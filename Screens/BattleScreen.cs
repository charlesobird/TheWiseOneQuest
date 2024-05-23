using GeonBit.UI;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using TheWiseOneQuest.Components;
using GeonBit.UI.Entities;
using TheWiseOneQuest.Models;
using System.Collections.Generic;
using System;
namespace TheWiseOneQuest.Screens;

public class BattleScreen : Menu
{
    List<Button> actionButtons = new();
    WizardInfo playerInfo;
    WizardInfo enemyInfo;
    public void LockActionButtons()
    {
        foreach (var button in actionButtons)
        {
            button.Enabled = true;
        }
    }
    public void UnlockActionButtons()
    {
        foreach (var button in actionButtons)
        {
            button.Enabled = false;
        }
    }
    public void UpdateHealthBars()
    {
        playerInfo.wizardHealth.Value = (int)Core.battleHandler.currPlayerHealth;
        enemyInfo.wizardHealth.Value = (int)Core.battleHandler.currEnemyHealth;
        playerInfo.wizHealthParagraph.Text = $"Health: {Core.battleHandler.currPlayerHealth} / {Core.playerWizard.MaxHealth}";
        enemyInfo.wizHealthParagraph.Text = $"Health: {Core.battleHandler.currEnemyHealth} / {Core.battleHandler.enemyWizard.MaxHealth}";
    }
    public void DestroyBattleScreen()
    {
        RemoveFromParent();
    }
    public BattleScreen()
    {
        Scale = 1f;
        _indexInParent = -1;
        Opacity = 1;
        Size = new Vector2(0, 0);

        playerInfo = new WizardInfo(Core.playerWizard)
        {
            Anchor = Anchor.TopLeft
        };
        if (Core.battleHandler.playerAdvantage)
        {
            Paragraph advantageParagraph = new("You have a 50% damage bonus this attack!")
            {
                Anchor = Anchor.AutoCenter
            };
            playerInfo.AddChild(advantageParagraph);
        }
        enemyInfo = new WizardInfo(Core.battleHandler.enemyWizard)
        {
            Anchor = Anchor.TopRight
        };
        if (Core.battleHandler.playerAdvantage)
        {
            Paragraph advantageParagraph = new("You have a 50% damage bonus this attack!")
            {
                Anchor = Anchor.AutoCenter
            };
            enemyInfo.AddChild(advantageParagraph);
        }
        Button attackButton = new("Attack!")
        {
            OnClick = (e) =>
            {
                Core.battleHandler.HandleAttack();
            }
        };
        Button blockButton = new("Block!")
        {
            OnClick = (e) =>
            {
                Core.battleHandler.HandleBlock(true);
            }
        };

        actionButtons.Add(attackButton);
        actionButtons.Add(blockButton);

        Panel actionPanel = new(
            new Vector2(0.25f),
            PanelSkin.None,
            Anchor.BottomCenter
        );

        AddChild(playerInfo);
        AddChild(enemyInfo);

        // Action Buttons
        foreach (Button actionBtn in actionButtons)
        {
            actionPanel.AddChild(actionBtn);
        }
        Button returnToMenu = new("Main Menu")
        {
            OnClick = (Entity bt) =>
            {
                Core.exitGame.Visible = true;
                Core.spriteHandler.ClearAnimatedSprites();
                UserInterface.Active.AddEntity(new MainMenu());
                RemoveFromParent();
            }
        };
        actionPanel.AddChild(returnToMenu);
        AddChild(actionPanel);


    }
    public void InitBattleScreen(bool playerGoesFirst)
    {
        if (!playerGoesFirst)
        {
            Core.battleHandler.HandleAttack(true);
        }
    }
}
