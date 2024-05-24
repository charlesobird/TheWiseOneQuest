

namespace TheWiseOneQuest.Handlers;

public class WizardHandler
{
	private static Dictionary<string, PlayerWizard> Wizards { get; set; }
	private JsonHandler storage = new();

	public WizardHandler()
	{
		Wizards = null;
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
		// Gets the names from the EnemyResources
		string[] enemyNames = EnemyResources.names;
		// Gets a random name from the EnemyResources.names
		string enemyName = enemyNames.ElementAt(_Utils.GenerateRandomInteger(maxValue: enemyNames.Length));
		// Gets the descriptors from the EnemyResources
		string[] enemyDescriptors = EnemyResources.descriptors;
		// Gets a random descriptor from the EnemyResources.descriptors
		string enemyDescriptor = enemyDescriptors.ElementAt(
			_Utils.GenerateRandomInteger(maxValue: enemyDescriptors.Length)
		);
		// Create the name by joining the Descriptor and Name
		enemyName = $"{enemyDescriptor} {enemyName}";
		// Create the Enemy Wizard and Generate some Stats for them
		EnemyWizard enemyWizard = new EnemyWizard(enemyName);
		enemyWizard.CreateStats();
		return enemyWizard;
	}
}