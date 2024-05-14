using GeonBit.UI;
using GeonBit.UI.Entities;
using GeonBit.UI.Entities.TextValidators;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;
using TheWiseOneQuest.Models;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest.Screens;

public class CharacterCreation : Menu
{
    static Paragraph availPointsParagraph;
    static Label hpLabel;
    static Paragraph hpParagraph;
    static Label wisPointLabel;
    static Paragraph wisPointParagraph;
    static Label dexPointLabel;
    static Paragraph dexPointParagraph;
    static Paragraph maxHealthParagraph;

    // {Hp,Wisdom,Dexterity,MaxHealth}
    public int[] wizardStats = new int[4] { 0, 0, 0, _Utils.DEFAULT_MAX_HEALTH };

    public int availablePoints = _Utils.DEFAULT_STARTER_POINTS;

    private void UpdateParagraphs()
    {
        hpParagraph.Text = $"{wizardStats[0]}";
        wisPointParagraph.Text = $"{wizardStats[1]}";
        dexPointParagraph.Text = $"{wizardStats[2]}";
        maxHealthParagraph.Text = $"Max Health: {wizardStats[3]}";
        availPointsParagraph.Text = $"Available Points: {availablePoints}";
    }

    public CharacterCreation()
    {
        // Panel Properties
        Size = new Vector2(0.5f, 0.75f);
        // Entity Creation and Setup
        Header header = new Header("Create New Character") { FillColor = Color.White };
        Panel entitiesGroup = new Panel(new Vector2(0.35f, -1), PanelSkin.None, Anchor.AutoCenter)
        {
            Padding = Vector2.Zero
        };

        var columnPanels = PanelsGrid.GenerateColums(3, entitiesGroup);
        foreach (var column in columnPanels)
        {
            column.Padding = Vector2.Zero;
        }

        Panel leftPanel = columnPanels[0];
        Panel centerPanel = columnPanels[1];
        Panel rightPanel = columnPanels[2];
        TextInput nameInput = new TextInput(false, new Vector2(0.4f, -1), anchor: Anchor.AutoCenter)
        {
            PlaceholderText = "Name"
        };
        nameInput.Validators.Add(new EnglishCharactersOnly(true));
        nameInput.Validators.Add(new OnlySingleSpaces());
        nameInput.Validators.Add(new MakeTitleCase());

        maxHealthParagraph = new Paragraph(
            $"Max Health: {wizardStats[3]}",
            anchor: Anchor.AutoCenter
        );
        availPointsParagraph = new Paragraph(
            $"Available Points: {availablePoints}",
            anchor: Anchor.AutoCenter
        );
        hpLabel = new Label("Health", anchor: Anchor.AutoCenter);
        hpParagraph = new Paragraph($"{wizardStats[0]}", anchor: Anchor.AutoCenter);
        Icon hpSkillIncrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.PLUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (availablePoints != 0)
                {
                    wizardStats[0]++;
                    wizardStats[3] += wizardStats[0];
                    availablePoints--;
                    UpdateParagraphs();
                }
            }
        };
        Icon hpSkillDecrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.MINUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (wizardStats[0] > 0)
                {
                    wizardStats[0]--;
                    wizardStats[3] -= wizardStats[0];
                    availablePoints++;
                    UpdateParagraphs();
                }
            }
        };
        wisPointLabel = new Label("Wisdom", anchor: Anchor.AutoCenter);
        wisPointParagraph = new Paragraph($"{wizardStats[1]}", anchor: Anchor.AutoCenter);
        Icon wisSkillIncrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.PLUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (availablePoints != 0)
                {
                    wizardStats[1]++;
                    availablePoints--;
                    UpdateParagraphs();
                }
            }
        };
        Icon wisSkillDecrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.MINUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (wizardStats[1] > 0)
                {
                    wizardStats[1]--;
                    availablePoints++;
                    UpdateParagraphs();
                }
            }
        };
        dexPointLabel = new Label("Dexterity", anchor: Anchor.AutoCenter);
        dexPointParagraph = new Paragraph($"{wizardStats[2]}", anchor: Anchor.AutoCenter);
        Icon dexSkillIncrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.PLUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (availablePoints != 0)
                {
                    wizardStats[2]++;
                    availablePoints--;
                    UpdateParagraphs();
                }
            }
        };
        Icon dexSkillDecrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = _Utils.MINUS_ICON,
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (wizardStats[2] > 0)
                {
                    wizardStats[2]--;
                    availablePoints++;
                    UpdateParagraphs();
                }
            }
        };

        Button createCharacterButton = new Button("Create Wizard")
        {
            Visible = true,
            Anchor = Anchor.AutoCenter,
            Size = new Vector2(0.4f, -1),
            OnClick = (Entity e) =>
            {
                string wizardName = nameInput.TextParagraph.Text;
                // check for name input
                if (wizardName.Length > 0)
                {
                    MessageBox.ShowYesNoMsgBox(
                        "Confirmation",
                        $"Are you sure you want the following for your new Wizard?\n\nName: {wizardName}\nStats:\n Hp: {wizardStats[0]}\n Wisdom: {wizardStats[1]}\n Dexterity: {wizardStats[2]}\n Max Health: {wizardStats[3]}",
                        () =>
                        {
                            PlayerWizard playerWizard = Core.wizardHandler.CreateWizard(wizardName);
                            playerWizard.Hp = wizardStats[0];
                            playerWizard.Wisdom = wizardStats[1];
                            playerWizard.Dexterity = wizardStats[2];
                            playerWizard.MaxHealth = wizardStats[3];
                            Core.wizardHandler.SaveWizardState(wizardName, playerWizard);
                            Core.SetPlayerWizard(playerWizard);
                            Core.exitGame.Visible = true;
                            RemoveFromParent();
                            Core.ShowBattleScreen();
                            return true;
                        },
                        () =>
                        {
                            return true;
                        }
                    );
                }
            }
        };

        // Children Adding
        AddChild(header);
        AddChild(new HorizontalLine());
        AddChild(new Paragraph("Name", size: new Vector2(0.4f, 0.4f), anchor: Anchor.AutoCenter));
        AddChild(nameInput);
        AddChild(entitiesGroup);
        // Health Points
        centerPanel.AddChild(hpLabel);
        leftPanel.AddChild(hpSkillIncrease);
        centerPanel.AddChild(hpParagraph);
        rightPanel.AddChild(hpSkillDecrease);
        // // Wisdom Points
        centerPanel.AddChild(wisPointLabel);
        leftPanel.AddChild(wisSkillIncrease);
        centerPanel.AddChild(wisPointParagraph);
        rightPanel.AddChild(wisSkillDecrease);
        // Dexterity Points
        centerPanel.AddChild(dexPointLabel);
        leftPanel.AddChild(dexSkillIncrease);
        centerPanel.AddChild(dexPointParagraph);
        rightPanel.AddChild(dexSkillDecrease);

        AddChild(maxHealthParagraph);
        AddChild(availPointsParagraph);
        AddChild(createCharacterButton);
        AddReturnButton(
            (Entity e) =>
            {
                UserInterface.Active.AddEntity(new PlaySelection());
            }
        );
    }
}
