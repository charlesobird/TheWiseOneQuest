using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TheWiseOneQuest.Models;

namespace TheWiseOneQuest.Handlers;

public class DatabaseHandler
{
    readonly SQLiteAsyncConnection _database;

    public DatabaseHandler()
    {
        string dbFilePath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.ApplicationData
        );
        dbFilePath = Path.Combine(dbFilePath, "wizards.sqlite");
        _database = new SQLiteAsyncConnection(dbFilePath);
        _database.CreateTableAsync<DBPlayerWizard>().Wait();
    }

    public async Task<bool> AddAsync(DBPlayerWizard playerWizard)
    {
        await _database.InsertAsync(playerWizard);

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _database.DeleteAsync(id);

        return await Task.FromResult(true);
    }

    public async Task<DBPlayerWizard> GetAsync(string id)
    {
        return await _database.GetAsync<DBPlayerWizard>(id);
    }

    public async Task<List<DBPlayerWizard>> GetAllWizards()
    {
        return await _database.Table<DBPlayerWizard>().ToListAsync();
    }
}
