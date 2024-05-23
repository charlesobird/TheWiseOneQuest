namespace TheWiseOneQuest.Models;

public class Wizard
{
	public byte Hp { get; set; }
	public int MaxHealth { get; set; }
	public byte Dexterity { get; set; }
	public byte Wisdom { get; set; }
	public string Name { get; set; }

	public Wizard()
	{
        Hp = 0;
        Dexterity = 0;
        Wisdom = 0;
		MaxHealth = _Utils.DEFAULT_MAX_HEALTH;
	}

	public bool CheckIfBuffActive(Element nonAttackingWizardElement, Element attackingWizardElement)
	{
		if (nonAttackingWizardElement == Element.Water && attackingWizardElement == Element.Fire)
		{
			return true;
		}
		else if (nonAttackingWizardElement == Element.Fire && attackingWizardElement == Element.Air)
		{
			return true;
		}
		else if (nonAttackingWizardElement == Element.Air && attackingWizardElement == Element.Earth)
		{
			return true;
		}
		else if (nonAttackingWizardElement == Element.Earth && attackingWizardElement == Element.Water)
		{
			return true;
		}
		return false;
	}
}
