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
        Hp = (byte)_Utils.GenerateRandomInteger(1,10);
        Dexterity = (byte)_Utils.GenerateRandomInteger(1,10);
        Wisdom = (byte)_Utils.GenerateRandomInteger(10,20);
        MaxHealth = 100 + Hp;
    }
}
