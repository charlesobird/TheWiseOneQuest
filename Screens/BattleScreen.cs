using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TheWiseOneQuest.Handlers;
using TheWiseOneQuest.Models;
using Core = TheWiseOneQuest.TheWiseOneQuest;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _Utils = TheWiseOneQuest.Utils.Utils;
namespace TheWiseOneQuest.Screens;

public class BattleScreen : Panel
{
    public BattleScreen()
    {
        Scale = 1f;
        _indexInParent = -1;
        Opacity = 1;
        Size = new Vector2(0, 0);
        Button returnToMenu = new Button("To Main Menu", ButtonSkin.Default, Anchor.BottomCenter,size:new Vector2(0.25f,0.05f))
        {
            OnClick = (Entity bt) =>
            {
                Core.exitGame.Visible = true;
                RemoveFromParent();
            }
        };
        AddChild(returnToMenu);
    }
}
