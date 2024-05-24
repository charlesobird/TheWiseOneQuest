namespace TheWiseOneQuest.Screens;

public class SettingsMenu : Menu
{
    public SettingsMenu(GraphicsDeviceManager graphics)
    {
        _anchor = Anchor.Center;
        Header header = new Header("Settings") { FillColor = Color.White };
        AddChild(header);
        AddChild(new HorizontalLine());
        CheckBox muteMusic = new CheckBox(
            "Mute Music",
            isChecked: Core.musicHandler.Paused
        )
        {
            OnClick = (Entity bt) =>
            {
                if (!Core.musicHandler.Paused)
                {
                    Core.musicHandler.Pause();
                }
                else
                {
                    Core.musicHandler.Resume();
                }
            }
        };
        AddChild(muteMusic);
        Slider volumeSlider = new(min: 0, max: 100){
            Value = (int)(MusicHandler.Volume * 100)
        };
        byte n = Convert.ToByte("1");
        volumeSlider.OnValueChange = (e) =>
        {
            Core.musicHandler.SetVolume(volumeSlider.Value);
        };
        AddChild(volumeSlider);
        // fullScreenToggle.ToolTipText = "Toggle fullscreen";
        // fullScreenToggle.OnClick = (Entity bt) =>
        // {
        //     //_Utils.ToggleFullscreen();
        //     fullScreenToggle.ChangeValue(graphics.IsFullScreen, true);
        // };
        // AddChild(fullScreenToggle);
        AddReturnButton((Entity e) =>
        {
            UserInterface.Active.AddEntity(new MainMenu());
        });
    }

}
