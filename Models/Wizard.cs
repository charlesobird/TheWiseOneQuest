using System;
using System.Runtime.CompilerServices;
using _Utils = TheWiseOneQuest.Utils.Utils;
namespace TheWiseOneQuest.Models;

public class Wizard
{
	public int Hp { get; set; }
	public int MaxHealth { get; set; }
	public int Dexterity { get; set; }
	public int Wisdom { get; set; }
	public Wizard()
	{
		MaxHealth = _Utils.DEFAULT_MAX_HEALTH;
	}
	public void CreateStats()
	{
		double hpRandom = _Utils.GenerateRandomDouble();
		double dexRandom = _Utils.GenerateRandomDouble();
		double wisRandom = _Utils.GenerateRandomDouble();
		Hp = (int)Math.Floor((1 + hpRandom) * _Utils.DEFAULT_HP_STAT);
		Dexterity = (int)Math.Floor((1 + dexRandom) * _Utils.DEFAULT_DEX_STAT);
		Wisdom = (int)Math.Floor((1 + wisRandom) * _Utils.DEFAULT_WIS_STAT);
		MaxHealth = _Utils.DEFAULT_MAX_HEALTH + (2 * Hp);
	}
	public bool CheckIfBuffActive(string nonAttackingWizardElement, string attackingWizardElement)
	{
		if (nonAttackingWizardElement == "Water" && attackingWizardElement == "Fire")
		{
			return true;
		}
		else if (nonAttackingWizardElement == "Fire" && attackingWizardElement == "Air")
		{
			return true;
		}
		else if (nonAttackingWizardElement == "Air" && attackingWizardElement == "Earth")
		{
			return true;
		}
		else if (nonAttackingWizardElement == "Earth" && attackingWizardElement == "Water")
		{
			return true;
		}
		return false;
	}
}
