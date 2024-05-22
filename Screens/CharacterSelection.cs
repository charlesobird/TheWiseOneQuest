using System;
using System.Collections.Generic;
using System.Linq;
using GeonBit.UI;
using GeonBit.UI.Entities;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;
using TheWiseOneQuest.Models;
using Core = TheWiseOneQuest.TheWiseOneQuest;

namespace TheWiseOneQuest.Screens;

public class CharacterSelection : Menu
{
    public PlayerWizard selectedWizard = null;

    public CharacterSelection()
    {
        _anchor = Anchor.Center;
        _scrollbar = new VerticalScrollbar();
        Header header = new Header("Character Selection") { FillColor = Color.White };
        AddChild(header);
        AddChild(new HorizontalLine());
    }

    public void SetupUI(Dictionary<string, PlayerWizard> wizards)
    {
        if (wizards.ToArray().Length == 0)
            return;
        int panelSize = (int)Convert.ToDouble(730 / wizards.ToArray().Length);
        DropDown wizardDropdown = new DropDown();
        SelectList chosenWizardStats = new SelectList() { Locked = true };
        bool firstClick = false;
        wizardDropdown.OnClick = (Entity e) =>
        {
            if (wizardDropdown.IsFocused && !firstClick)
            {
                wizardDropdown.ClearItems();
                foreach (KeyValuePair<string, PlayerWizard> wizard in wizards)
                {
                    wizardDropdown.AddItem(wizard.Key);
                    wizards[wizard.Key].SetName(wizard.Key);
                }
                firstClick = true;
            }
        };

        string wizardName;
        AddChild(wizardDropdown);
        AddChild(chosenWizardStats);
        Button selectCharacter =
            new("Select Character")
            {
                Visible = false,
                OnClick = (Entity e) =>
                {
                    if (selectedWizard == null)
                        return;
                    Core.SetPlayerWizard(selectedWizard);
                    RemoveFromParent();
                    Core.ShowBattleScreen();
                }
            };

        AddChild(selectCharacter);
        wizardDropdown.OnValueChange = (Entity entity) =>
        {
            wizardName = wizardDropdown.GetValue().ToString();
            PlayerWizard wizard = wizards[wizardName];
            chosenWizardStats.ClearItems();
            chosenWizardStats.AddItem(string.Format("{0} {1,-8}", "Name", wizard.Name));
            chosenWizardStats.AddItem(string.Format("{0} {1,-8}", "Dexterity", wizard.Dexterity));
            chosenWizardStats.AddItem(string.Format("{0} {1,-8}", "Wisdom", wizard.Wisdom));
            chosenWizardStats.AddItem(string.Format("{0} {1,-8}", "Hp", wizard.Hp));
            chosenWizardStats.AddItem(string.Format("{0} {1,-8}", "Max Health", wizard.MaxHealth));
            selectedWizard = wizard;
            selectedWizard.SetName(wizardName);
            selectCharacter.Visible = true;
        };

        AddReturnButton(
            (Entity e) =>
            {
                UserInterface.Active.AddEntity(new PlaySelection());
            }
        );
    }
}
