namespace TheWiseOneQuest.Screens;

public class BattleScreen : Menu
{
    public List<Button> actionButtons = new();
    public WizardInfo playerInfo;
    public WizardInfo enemyInfo;
    public void LockActionButtons()
    {

        foreach (var button in actionButtons)
        {
            button.Enabled = false;
        }
    }
    public void UnlockActionButtons()
    {
        foreach (var button in actionButtons)
        {
            button.Enabled = true;
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

        enemyInfo = new WizardInfo(Core.battleHandler.enemyWizard)
        {
            Anchor = Anchor.TopRight
        };
        Panel headerPanel = new(
            size: new Vector2(0.25f,0.15f),
            anchor:Anchor = Anchor.TopCenter
        ){
            MaxSize = new Vector2(0.35f,0.25f)
        };
        headerPanel.AddChild(new Header($"Round {Core.playerWizard.RoundsPlayed + 1} of {_Utils.DEFAULT_ROUNDS_WON_THRESHOLD}",Anchor.Center));
        Button attackButton = new("Attack!")
        {
            OnClick = async (e) =>
            {
                LockActionButtons();
                await Core.battleHandler.AttackAsync();
                UnlockActionButtons();
            },
            Anchor = Anchor.AutoInlineNoBreak
        };
        Button blockButton = new("Block!")
        {
            OnClick = (e) =>
            {
                LockActionButtons();
                Core.battleHandler.HandleBlock(true);
            },
            Anchor = Anchor.AutoInlineNoBreak
        };
        Button healButton = new("Heal Yourself!")
        {
            OnClick = (e) =>
            {
                LockActionButtons();
                Core.battleHandler.HandleHeal(true);
            },
            Anchor = Anchor.AutoInlineNoBreak
        };

        Panel actionPanel = new(
            new Vector2(0.5f, 0.20f),
            PanelSkin.None,
            Anchor.BottomCenter,
            Vector2.Zero
        );
        actionButtons.Add(attackButton);
        actionButtons.Add(healButton);
        actionButtons.Add(blockButton);

        AddChild(headerPanel);
        AddChild(playerInfo);
        AddChild(enemyInfo);
        Button returnToMenu = new("Main Menu")
        {
            OnClick = (Entity bt) =>
            {
                Core.exitGame.Visible = true;
                Core.spriteHandler.ClearAnimatedSprites();
                Core.projectileHandler.ClearElementalMoves();
                UserInterface.Active.AddEntity(new MainMenu());
                Core.battleHandler = new();
                RemoveFromParent();
            }
        };
        var columnPanels = PanelsGrid.GenerateColums(3, actionPanel);
        foreach (var column in columnPanels)
        {
            column.Padding = Vector2.Zero;
        }

        Panel leftPanel = columnPanels[0];
        Panel centerPanel = columnPanels[1];
        Panel rightPanel = columnPanels[2];
        leftPanel.AddChild(attackButton);
        centerPanel.AddChild(healButton);
        centerPanel.AddChild(returnToMenu);
        rightPanel.AddChild(blockButton);
        AddChild(actionPanel);


    }
    public async Task InitBattleScreen(bool playerGoesFirst)
    {
        if (!playerGoesFirst)
        {
            LockActionButtons();
            await Core.battleHandler.AttackAsync();

        }
    }
}
