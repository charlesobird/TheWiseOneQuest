using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;
using TheWiseOneQuest.Handlers;
using System.Threading;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using System;
using System.Collections.Generic;
using TheWiseOneQuest.Models;
using System.Linq;
using System.Diagnostics;
using System.Collections;

namespace TheWiseOneQuest.Screens;

public class PlaySelection : Menu
{
    public static WizardHandler wizardHandler;
    public PlaySelection()
    {
        _anchor = Anchor.Center;
        Size = new Vector2(
            _Utils.GetPercentageOfScreenWidth(0.35),
            _Utils.GetPercentageOfScreenHeight(0.65)
        );

        wizardHandler = new();
        Dictionary<string, PlayerWizard> wizards = wizardHandler.GetWizards();
        Header header = new Header("Selection");
        header.FillColor = Color.White;
        AddChild(header);
        AddChild(new HorizontalLine());

        Button chooseCharacter = new("Choose Your Character");
        if (DictionaryBase.ReferenceEquals(wizards,null)) {
            chooseCharacter.Visible = false;
        }
        Button createCharacter = new("Create A New Character");
        chooseCharacter.OnClick = (Entity e) =>
        {
            CharacterSelection characterSelection = new CharacterSelection();
            UserInterface.Active.AddEntity(characterSelection);
            Thread.Sleep(20);
            characterSelection.SetupUI(wizards);
        };
        createCharacter.OnClick = (Entity e) =>
        {
            CharacterCreation characterCreation = new CharacterCreation();
            UserInterface.Active.AddEntity(characterCreation);
        };
        AddChild(chooseCharacter);
        AddChild(createCharacter);
        AddReturnButton();
    }
}
