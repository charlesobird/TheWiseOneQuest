using System;
using GeonBit.UI.Entities;

namespace TheWiseOneQuest.Components;

public class Menu : Panel
{
    public Menu()
    {
        Scale = 1f;
        Size = new Microsoft.Xna.Framework.Vector2(0.5f);
        Anchor = Anchor.Center;
    }

    public void AddReturnButton(Action<Entity> onclick)
    {
        Button returnToMenu = new Button("Return", ButtonSkin.Default, Anchor.BottomCenter)
        {
            OnClick = (Entity entity) =>
            {
                RemoveFromParent();
                onclick.Invoke(entity);
            }
        };
        AddChild(returnToMenu);
    }
    public void AddReturnButton()
    {
        Button returnToMenu = new Button("Return", ButtonSkin.Default, Anchor.BottomCenter)
        {
            OnClick = (Entity entity) =>
            {
                RemoveFromParent();
            }
        };
        AddChild(returnToMenu);
    }
}
