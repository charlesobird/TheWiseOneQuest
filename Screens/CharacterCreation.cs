using System;
using GeonBit.UI.Entities;
using GeonBit.UI.Entities.TextValidators;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Components;
using TheWiseOneQuest.Models;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;
using GeonBit.UI;

namespace TheWiseOneQuest.Screens;

public class CharacterCreation : Menu
{
    static int availableSkillPoints = _Utils.DEFAULT_STARTER_POINTS;
    static Paragraph availPointsParagraph;
    static Paragraph hpPointParagraph;
    static Paragraph wisPointParagraph;
    static Paragraph dexPointParagraph;
    static Paragraph maxHealthParagraph;

    public CharacterCreation()
    {
        Size = new Vector2(0.5f, 0.65f);
        Header header = new Header("Create New Character") { FillColor = Color.White };
        AddChild(header);
        AddChild(new HorizontalLine());
        AddChild(new Paragraph("Name:", size: new Vector2(0.4f, 0.4f), anchor: Anchor.AutoCenter));
        TextInput nameInput = new TextInput(false, new Vector2(0.4f, -1), anchor: Anchor.AutoCenter)
        {
            PlaceholderText = "Name"
        };
        nameInput.Validators.Add(new EnglishCharactersOnly(true));
        nameInput.Validators.Add(new OnlySingleSpaces());
        nameInput.Validators.Add(new MakeTitleCase());
        AddChild(nameInput);

        PlayerWizard playerWizard = Core.wizardHandler.CreateWizard(nameInput.TextParagraph.Text);
        maxHealthParagraph = new Paragraph(
            $"Max Health: {playerWizard.MaxHealth}",
            anchor: Anchor.AutoCenter
        );
        availPointsParagraph = new Paragraph(
            $"Available Points: {availableSkillPoints}",
            anchor: Anchor.AutoCenter
        );
        hpPointParagraph = new Paragraph("Health: 0", anchor: Anchor.AutoCenter);
        Slider hpSkillSlider = new Slider(0, availableSkillPoints)
        {
            Value = 0,
            Size = new Vector2(0.5f, -1),
            Anchor = Anchor.AutoCenter
        };
        hpSkillSlider.OnValueChange = (Entity e) =>
        {
            if (availableSkillPoints > 0)
            {
                hpSkillSlider.Locked = false;
                if (
                    playerWizard.Hp > hpSkillSlider.Value
                    && ((playerWizard.Hp - availableSkillPoints) > 0)
                )
                {
                    availableSkillPoints -= hpSkillSlider.Value;
                }
                else
                {
                    availableSkillPoints += hpSkillSlider.Value;
                }
                hpPointParagraph.Text = $"Health: {hpSkillSlider.Value}";
                playerWizard.Hp = hpSkillSlider.Value;
                availableSkillPoints -= hpSkillSlider.Value;
                hpSkillSlider.Max = availableSkillPoints;
                availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
            }
            else
            {
                hpSkillSlider.Locked = true;
            }
        };
        Slider wisSkillSlider = new Slider(0, availableSkillPoints)
        {
            Value = 0,
            Size = new Vector2(0.5f, -1),
            Anchor = Anchor.AutoCenter
        };
        wisSkillSlider.OnValueChange = (Entity e) =>
        {
            if (availableSkillPoints > 0)
            {
                wisSkillSlider.Locked = false;
                if (
                    playerWizard.Wisdom > wisSkillSlider.Value
                    && ((playerWizard.Wisdom - availableSkillPoints) > 0)
                )
                {
                    availableSkillPoints -= wisSkillSlider.Value;
                }
                else
                {
                    availableSkillPoints += wisSkillSlider.Value;
                }
                wisPointParagraph.Text = $"Wisdom: {wisSkillSlider.Value}";
                playerWizard.Wisdom = wisSkillSlider.Value;
                availableSkillPoints -= wisSkillSlider.Value;
                wisSkillSlider.Max = availableSkillPoints;
                availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
            }
            else
            {
                wisSkillSlider.Locked = true;
            }
        };
        Slider dexSkillSlider = new Slider(0, availableSkillPoints)
        {
            Value = 0,
            Size = new Vector2(0.5f, -1),
            Anchor = Anchor.AutoCenter
        };
        dexSkillSlider.OnValueChange = (Entity e) =>
        {
            if (availableSkillPoints > 0)
            {
                dexSkillSlider.Locked = false;
                if (
                    playerWizard.Dexterity > dexSkillSlider.Value
                    && ((playerWizard.Dexterity - availableSkillPoints) > 0)
                )
                {
                    availableSkillPoints -= dexSkillSlider.Value;
                }
                else
                {
                    availableSkillPoints += dexSkillSlider.Value;
                }
                dexPointParagraph.Text = $"Dexterity: {dexSkillSlider.Value}";
                playerWizard.Dexterity = dexSkillSlider.Value;
                availableSkillPoints -= dexSkillSlider.Value;
                dexSkillSlider.Max = availableSkillPoints;
                availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
            }
            else
            {
                dexSkillSlider.Locked = true;
            }
        };
        // Icon hpSkillIncrease = new Icon()
        // {
        // 	IconType = IconType.None,
        // 	TextureName = "icons/plus",
        // 	Anchor = Anchor.AutoCenter,
        // 	OnClick = (Entity e) =>
        // 	{
        // 		if (availableSkillPoints != 0)
        // 		{
        // 			playerWizard.Hp++;
        // 			playerWizard.MaxHealth += 2 * playerWizard.Hp;
        // 			hpPointParagraph.Text = $"Health: {playerWizard.Hp}";
        // 			maxHealthParagraph.Text = $"Max Health: {playerWizard.MaxHealth}";
        // 			availableSkillPoints--;
        // 			availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
        // 		}
        // 	}
        // };
        // Icon hpSkillDecrease = new Icon()
        // {
        // 	IconType = IconType.None,
        // 	TextureName = "icons/minus",
        // 	Anchor = Anchor.AutoCenter,
        // 	OnClick = (Entity e) =>
        // 	{
        // 		if (playerWizard.Hp > 0)
        // 		{
        // 			playerWizard.Hp--;
        // 			hpPointParagraph.Text = $"Health: {playerWizard.Hp}";
        // 			playerWizard.MaxHealth = _Utils.DEFAULT_MAX_HEALTH + 2 * playerWizard.Hp;
        // 			maxHealthParagraph.Text = $"Max Health: {playerWizard.MaxHealth}";
        // 			availableSkillPoints++;
        // 			availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
        // 		}
        // 	}
        // };
        AddChild(hpPointParagraph);
        AddChild(hpSkillSlider);
        // AddChild(hpSkillIncrease);
        // AddChild(hpSkillDecrease);
        wisPointParagraph = new Paragraph("Wisdom: 0", Anchor.AutoCenter);
        Icon wisSkillIncrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = "icons/plus",
            Size = new Vector2(10),
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (availableSkillPoints != 0)
                {
                    playerWizard.Wisdom++;
                    wisPointParagraph.Text = $"Wisdom: {playerWizard.Wisdom}";
                    availableSkillPoints--;
                    availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
                }
            }
        };
        Icon wisSkillDecrease = new Icon()
        {
            IconType = IconType.None,
            TextureName = "icons/minus",
            Size = new Vector2(10),
            Anchor = Anchor.AutoCenter,
            OnClick = (Entity e) =>
            {
                if (playerWizard.Hp > 0)
                {
                    playerWizard.Wisdom--;
                    wisPointParagraph.Text = $"Wisdom: {playerWizard.Wisdom}";
                    availableSkillPoints++;
                    availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
                }
            }
        };
        AddChild(wisPointParagraph);
        AddChild(wisSkillSlider);
        // AddChild(wisSkillIncrease);
        // AddChild(wisSkillDecrease);
        dexPointParagraph = new Paragraph("Dexterity: 0", Anchor.AutoCenter);
        // Icon dexSkillIncrease = new Icon()
        // {
        // 	IconType = IconType.None,
        // 	TextureName = "icons/plus",
        // 	Size = new Vector2(10),
        // 	Anchor = Anchor.AutoCenter,
        // 	OnClick = (Entity e) =>
        // 	{
        // 		if (availableSkillPoints != 0)
        // 		{
        // 			playerWizard.Dexterity++;
        // 			dexPointParagraph.Text = $"Dexterity: {playerWizard.Dexterity}";
        // 			availableSkillPoints--;
        // 			availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
        // 		}
        // 	}
        // };
        // Icon dexSkillDecrease = new Icon()
        // {
        // 	IconType = IconType.None,
        // 	TextureName = "icons/minus",
        // 	Anchor = Anchor.AutoCenter,
        // 	Size = new Vector2(10),
        // 	OnClick = (Entity e) =>
        // 	{
        // 		if (playerWizard.Hp > 0)
        // 		{
        // 			playerWizard.Dexterity--;
        // 			dexPointParagraph.Text = $"Dexterity: {playerWizard.Dexterity}";
        // 			availableSkillPoints++;
        // 			availPointsParagraph.Text = $"Available Points: {availableSkillPoints}";
        // 		}
        // 	}
        // };
        AddChild(dexPointParagraph);
        AddChild(dexSkillSlider);

        // AddChild(dexSkillIncrease);
        // AddChild(dexSkillDecrease);
        AddChild(maxHealthParagraph);
        AddChild(availPointsParagraph);
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
                    playerWizard.SetName(wizardName);
                    Core.wizardHandler.SaveWizardState(wizardName, playerWizard);
                    Core.exitGame.Visible = true;
                    RemoveFromParent();
                }
            }
        };
        AddChild(createCharacterButton);
        // Button returnToMenu = new Button("Return", ButtonSkin.Default, Anchor.BottomCenter)
        // {
        //     OnClick = (Entity bt) =>
        //     {
        //         RemoveFromParent();
        //     }
        // };
        // AddChild(returnToMenu);
        AddReturnButton((Entity e) => {
            UserInterface.Active.AddEntity(new PlaySelection());
        });
    }
}
