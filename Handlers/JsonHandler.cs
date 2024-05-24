using Newtonsoft.Json;

namespace TheWiseOneQuest.Handlers;

public class JsonHandler
{
    public string GetFullFilePath(string fileName)
    {
		return fileName;
    }

    public void CreateFile(string fileName)
    {
        try
        {
            File.Create(GetFullFilePath(fileName));
        }
        catch
        {
            throw;
        }
    }
    public dynamic ReadFromFile<T>(string fileName)
    {
        try
        {
            fileName = GetFullFilePath(fileName);
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
        catch (IOException)
        {
            throw;
        }
    }
    public void AppendToFile<T>(string fileName, string key, T contentToAdd)
    {
        try
        {
            var jsonContents = ReadFromFile<Dictionary<string, T>>(fileName);
            jsonContents[key] = contentToAdd;
            File.WriteAllText(
                GetFullFilePath(fileName),
                JsonConvert.SerializeObject(jsonContents, Formatting.Indented)
            );
        }
        catch (FileNotFoundException)
        {
            throw;
        }
        catch (NullReferenceException)
        {
            throw;
        }
    }
}
