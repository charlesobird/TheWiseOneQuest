using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using Microsoft.Xna.Framework;
using TheWiseOneQuest.Models;
using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest.Handlers;

public class WizardHandler
{
#nullable enable
	public static Dictionary<string, PlayerWizard>? Wizards { get; set; }
#nullable disable
	public JsonHandler storage = new();

	public WizardHandler() { }

	public void FindWizardStore()
	{
		if (!File.Exists(_Utils.WIZARD_STORE_FILE_NAME))
		{
			storage.CreateFile(_Utils.WIZARD_STORE_FILE_NAME);
			//Thread.Sleep(20);
			//storage.WriteEmptyJSONFile(_Utils.WIZARD_STORE_FILE_NAME);
		}
	}

	public Dictionary<string, PlayerWizard> GetWizards()
	{
		if (ReferenceEquals(Wizards, null) || Wizards.Count == 0)
		{
			var wizards = storage.ReadFromFile<Dictionary<string, PlayerWizard>>(
				_Utils.WIZARD_STORE_FILE_NAME
			);
			return wizards;
		}
		else
		{
			return Wizards;
		}
	}

	public PlayerWizard GetWizard(string name)
	{
		Dictionary<string, PlayerWizard> wizards = GetWizards();

		PlayerWizard matchingWizard = wizards.ContainsKey(name)
			? wizards[name]
			: CreateWizard(name);
		return matchingWizard;
	}

	public bool CheckForWizard(string name)
	{
		return Wizards.ContainsKey(name);
	}

	public PlayerWizard CreateWizard(string name)
	{
		PlayerWizard wizard = new PlayerWizard(name);
		SaveWizardState(name, wizard);
		return wizard;
	}

	public void SaveWizardState(string wizardName, PlayerWizard wizard)
	{
		storage.AppendToFile<PlayerWizard>(_Utils.WIZARD_STORE_FILE_NAME, wizardName, wizard);
		Wizards = GetWizards();
	}

	public EnemyWizard CreateEnemyWizard()
	{
		string[] enemyNames = storage.ReadFromFile<string[]>(@"enemyNames.json");
		string enemyName = enemyNames.ElementAt(_Utils.GenerateRandomInteger(enemyNames.Length));
		// string enemyElement = _Utils.ELEMENTS.ElementAt(
		// 	_Utils.GenerateRandomInteger(_Utils.ELEMENTS.Length)
		// );
		string[] enemyDescriptors = storage.ReadFromFile<string[]>(@"enemyDescriptors.json");
		string enemyDescriptor = enemyDescriptors.ElementAt(
			_Utils.GenerateRandomInteger(enemyDescriptors.Length)
		);
		enemyName = $"{enemyDescriptor} {enemyName}";
		EnemyWizard enemyWizard = new EnemyWizard(enemyName);
		enemyWizard.CreateStats();
		return enemyWizard;
	}
}
