using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Models;

namespace TheWiseOneQuest.Components;

class WizardInfo : Panel
{
	public WizardInfo(Wizard wizard)
	{
		Size = new Vector2(0.25f,0.2f);
		MaxSize = new Vector2(0.25f,0.2f);
		Paragraph wizardName = new Paragraph(wizard.Name);
		ProgressBar wizardHealth = new ProgressBar(0, wizard.MaxHealth)
		{
			Value = wizard.MaxHealth,
			Locked = true,
			SliderSkin = SliderSkin.Default
		};
		Paragraph wizHealthParagraph = new Paragraph($"Health: {wizard.MaxHealth} / {wizard.MaxHealth}", Anchor.Center);
		wizardHealth.AddChild(wizHealthParagraph);
		AddChild(wizardName);
		AddChild(wizardHealth);
	}
}
