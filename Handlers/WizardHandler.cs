using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheWiseOneQuest.Models;

using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest.Handlers;
public class WizardHandler
{
#nullable enable
	public static Dictionary<string, PlayerWizard>? Wizards { get; set; }
#nullable disable
	public JsonHandler storage = new();
	public DatabaseHandler database = new();
	public WizardHandler()
	{
		Wizards = GetWizards();
	}
	public void FindWizardStore()
	{
		if (!File.Exists(_Utils.WIZARD_STORE_FILE_NAME))
		{

			storage.CreateFile(_Utils.WIZARD_STORE_FILE_NAME);
		}
	}
	public Dictionary<string, PlayerWizard> GetWizards()
	{
		var wizards = storage.ReadFromFile<Dictionary<string, PlayerWizard>>(_Utils.WIZARD_STORE_FILE_NAME);
		return wizards;
	}

	public PlayerWizard GetWizard(string name, string element = "NONE")
	{
		Dictionary<string, PlayerWizard> wizards = GetWizards();

		PlayerWizard matchingWizard = wizards.ContainsKey(name) ? wizards[name] : CreateWizard(name, element);
		return matchingWizard;
	}

	public bool CheckForWizard(string name)
	{
		return Wizards.ContainsKey(name);
	}

	public PlayerWizard CreateWizard(string name, string element)
	{
		PlayerWizard wizard = new PlayerWizard(name, element);
		SaveWizardState(name, wizard);
		return wizard;
	}

	public void SaveWizardState(string wizardName, PlayerWizard wizard)
	{
		storage.WriteToFile<PlayerWizard>(_Utils.WIZARD_STORE_FILE_NAME, wizardName, wizard);
		Wizards = GetWizards();
	}

	public EnemyWizard CreateEnemyWizard()
	{
		string[] enemyNames = storage.ReadFromFile<string[]>(@"enemyNames.json");
		string enemyName = enemyNames.ElementAt(_Utils.GenerateRandomInteger(enemyNames.Length));
		string enemyElement = _Utils.ELEMENTS.ElementAt(_Utils.GenerateRandomInteger(_Utils.ELEMENTS.Length));
		string[] enemyDescriptors = storage.ReadFromFile<string[]>(@"enemyDescriptors.json");
		string enemyDescriptor = enemyDescriptors.ElementAt(_Utils.GenerateRandomInteger(enemyDescriptors.Length));
		enemyName = $"{enemyDescriptor} {enemyName}";
		EnemyWizard enemyWizard = new EnemyWizard(enemyName, enemyElement);
		enemyWizard.CreateStats();
		return enemyWizard;
	}

}
