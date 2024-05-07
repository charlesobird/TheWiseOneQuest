using System;
using System.Runtime.CompilerServices;
using _Utils = TheWiseOneQuest.Utils.Utils;
namespace TheWiseOneQuest.Models;

public class Wizard
{
	public int Hp { get; set; }
	public string Element { get; set; }

	public int MaxHealth { get; set; }
	public int Dexterity { get; set; }
	public int Wisdom { get; set; }
	public Wizard(string element)
	{
		Element = element;
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
	public bool CheckIfBuffActive(string attackingWizardElement)
	{
		if (Element == "Water" && attackingWizardElement == "Fire")
		{
			return true;
		}
		else if (Element == "Fire" && attackingWizardElement == "Air")
		{
			return true;
		}
		else if (Element == "Air" && attackingWizardElement == "Earth")
		{
			return true;
		}
		else if (Element == "Earth" && attackingWizardElement == "Water")
		{
			return true;
		}
		return false;
	}
}
