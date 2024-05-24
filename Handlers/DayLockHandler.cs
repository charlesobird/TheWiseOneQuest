namespace TheWiseOneQuest.Handlers;

public class DayLockHandler
{
    public DayLockHandler() { }

    private string GenerateFilePath(string wizName) {
        return $"FightLogs/{wizName.Replace(" ", "_")}_{DateTime.Today:yyyy_MM_dd}.txt";

    }
    public bool FindLogForPlayer(PlayerWizard playerWizard)
    {
        string filePath = GenerateFilePath(playerWizard.Name);
        return File.Exists(filePath);
    }

    public void CreateLogForPlayer(PlayerWizard playerWizard)
    {
        string filePath = GenerateFilePath(playerWizard.Name);
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }
}