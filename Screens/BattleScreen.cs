using GeonBit.UI;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using TheWiseOneQuest.Components;
using GeonBit.UI.Entities;
namespace TheWiseOneQuest.Screens;

public class BattleScreen : Menu
{
    public BattleScreen()
    {
        Scale = 1f;
        _indexInParent = -1;
        Opacity = 1;
        Size = new Vector2(0, 0);

        Panel playerInfo = new Panel(size:new Vector2(0.25f,-1),anchor:Anchor.BottomLeft, skin:PanelSkin.None);
        ProgressBar progressBar = new ProgressBar(0,Core.playerWizard.MaxHealth){
            Locked = true,
            SliderSkin = SliderSkin.Default
        };
        AddChild(playerInfo);
        playerInfo.AddChild(progressBar);
        Button returnToMenu = new("Main Menu", ButtonSkin.Default, Anchor.BottomCenter,size:new Vector2(0.25f,0.05f))
        {
            OnClick = (Entity bt) =>
            {
                Core.exitGame.Visible = true;
                Core.ResetSprites();
                UserInterface.Active.AddEntity(new MainMenu());
                RemoveFromParent();
            }
        };
        AddChild(returnToMenu);
    }
}
