using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Models;

namespace TheWiseOneQuest.Components;

class WizardInfo : Panel
{
	public Paragraph wizardName;
	public ProgressBar wizardHealth;
	public Paragraph wizHealthParagraph;
	public WizardInfo(Wizard wizard)
	{
		Size = new Vector2(0.25f,0.2f);
		MaxSize = new Vector2(0.25f,0.2f);
		wizardName = new Paragraph(wizard.Name);
		wizardHealth = new ProgressBar(0, wizard.MaxHealth)
		{
			Value = wizard.MaxHealth,
			Locked = true,
			SliderSkin = SliderSkin.Default
		};
		wizHealthParagraph = new Paragraph($"Health: {wizard.MaxHealth} / {wizard.MaxHealth}", Anchor.Center);
		wizardHealth.AddChild(wizHealthParagraph);
		AddChild(wizardName);
		AddChild(wizardHealth);
	}
}
