using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;
using GeonBit.UI;
using System;

namespace TheWiseOneQuest.Screens;

public class SettingsMenu : Menu
{
    public SettingsMenu(GraphicsDeviceManager graphics)
    {
        _anchor = Anchor.Center;
        Header header = new Header("Settings") { FillColor = Color.White };
        AddChild(header);
        AddChild(new HorizontalLine());
        CheckBox fullScreenToggle = new CheckBox(
            "Toggle Fullscreen",
            isChecked: graphics.IsFullScreen
        );
        fullScreenToggle.ToolTipText = "Toggle fullscreen";
        fullScreenToggle.OnClick = (Entity bt) =>
        {
            //_Utils.ToggleFullscreen();
            fullScreenToggle.ChangeValue(graphics.IsFullScreen, true);
        };
        AddChild(fullScreenToggle);
        AddReturnButton();
    }

}
