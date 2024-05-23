using System.Threading;
using GeonBit.UI.Animators;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
namespace TheWiseOneQuest.Components;

class Notification : Paragraph
{
    public Notification(string text)
    {
        //Skin = PanelSkin.Default;
        Offset = new Vector2(50);
        Size = new Vector2(0.5f, 0.1f);
        Anchor = Anchor.TopCenter;
        //Paragraph content = new(text, Anchor.AutoCenter);
        //AddChild(content);
        AttachAnimator(new FadeOutAnimator()
        {
            Enabled = true
        });
    }
}