using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;

namespace TheWiseOneQuest.Screens;

class MainMenu : Menu
{
    public MainMenu()
    {
        Header header = new Header("The Wise One's Quest") { FillColor = Color.White };
        AddChild(header);
        AddChild(new HorizontalLine());
        Button playButton = new Button("Play")
        {
            OnClick = (Entity e) =>
            {
                RemoveFromParent();
                PlaySelection playSelection = new PlaySelection();
                UserInterface.Active.AddEntity(playSelection);
            }
        };
        Button settingsButton = new Button("Settings")
        {
            OnClick = (Entity e) =>
            {
                RemoveFromParent();
                UserInterface.Active.AddEntity(new SettingsMenu(Core.graphics));
            }
        };

        AddChild(playButton);
        AddChild(settingsButton);
    }
}
