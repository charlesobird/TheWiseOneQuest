using _Utils = TheWiseOneQuest.Utils.Utils;

namespace TheWiseOneQuest.Models;


public class EnemyWizard : Wizard
{
    //public string Name { get; private set; }

    public EnemyWizard(string name)
    {
        Name = name;
    }

    public void CreateStats()
    {
        int availPoints = _Utils.DEFAULT_STARTER_POINTS / 2;
        Hp = (byte)_Utils.GenerateRandomInteger(availPoints);
        Dexterity = (byte)_Utils.GenerateRandomInteger(availPoints);
        Wisdom = (byte)_Utils.GenerateRandomInteger(availPoints);
        MaxHealth = 100 + Hp;
    }
}
