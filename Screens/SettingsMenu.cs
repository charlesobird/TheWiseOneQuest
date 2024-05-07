using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Components;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest.Screens;

public class SettingsMenu : Menu
{
  public SettingsMenu(GraphicsDeviceManager graphics)
  {
    _anchor = Anchor.Center;
    Size = new Vector2(_Utils.GetPercentageOfScreenWidth(0.35),_Utils.GetPercentageOfScreenHeight(0.65));
    Header header = new Header("Settings");
    header.FillColor = Color.White;
    AddChild(header);
    AddChild(new HorizontalLine());
    CheckBox fullScreenToggle = new CheckBox("Toggle Fullscreen",isChecked:graphics.IsFullScreen);
    fullScreenToggle.ToolTipText = "Toggle fullscreen";
    fullScreenToggle.OnClick = (Entity bt) =>
    {
      _Utils.ToggleFullscreen();
      fullScreenToggle.ChangeValue(graphics.IsFullScreen, true);
    };
    Button returnToMenu = new Button(
        "Return",
        ButtonSkin.Default,
        Anchor.BottomCenter
    );
    AddChild(fullScreenToggle);
    AddReturnButton();
  }
}
