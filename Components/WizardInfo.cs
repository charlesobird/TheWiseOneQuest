using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Models;

namespace TheWiseOneQuest.Components;

// For displaying a Wizard's Information (Name, health, advantages)
public class WizardInfo : Panel
{
	public Paragraph wizardName;
	public ProgressBar wizardHealth;
	public Paragraph wizHealthParagraph;
	public Paragraph infoParagraph;
	public WizardInfo(Wizard wizard)
	{
		Size = new Vector2(0.25f,0.2f);
		MaxSize = new Vector2(0.25f,0.2f);
		wizardName = new Paragraph(wizard.Name);
		wizardHealth = new ProgressBar(0, wizard.MaxHealth)
		{
			Value = wizard.MaxHealth,
			Locked = true, // This makes it so the player can't slide the progress bar manually with their mouse
			SliderSkin = SliderSkin.Default
		};
		wizHealthParagraph = new Paragraph($"Health: {wizard.MaxHealth} / {wizard.MaxHealth}", Anchor.Center);
		infoParagraph = new Paragraph("", Anchor = Anchor.AutoCenter);
		wizardHealth.AddChild(wizHealthParagraph);
		AddChild(wizardName);
		AddChild(wizardHealth);
		AddChild(infoParagraph);
	}
}
