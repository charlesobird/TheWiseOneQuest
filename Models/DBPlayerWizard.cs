namespace TheWiseOneQuest.Models;

public class DBPlayerWizard
{
    public string Name { get; set; }
    public string Element { get; set; }
    public int Hp { get; set; }
    public int MaxHealth { get; set; }
    public int Dexterity { get; set; }
    public int Wisdom { get; set; }
    public int RoundsWon { get; set; }
    public int RoundsPlayed { get; set; }
    public bool TheWiseOne { get; set; }
    public bool GameLost { get; set; }
}
