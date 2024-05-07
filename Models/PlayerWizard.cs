namespace TheWiseOneQuest.Models;

public class PlayerWizard : Wizard
{
	public int RoundsWon { get; set; }
	public int RoundsPlayed { get; set; }
	public bool TheWiseOne { get; set; }
	public bool GameLost { get; set; }
	public string Name { get; set; }

	public PlayerWizard(string name, string element)
		: base(element) { }
	public void SetName(string newName)
	{
		Name = newName;
	}
}
