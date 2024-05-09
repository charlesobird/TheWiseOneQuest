
namespace TheWiseOneQuest.Models;

public class EnemyWizard : Wizard
{
	public string Name { get; private set; }
	public EnemyWizard(string name) 
	{
		Name = name;
	}

}
