using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TheWiseOneQuest.Handlers;

public class JsonHandler
{
    public string ProjectDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "TheWiseOneQuest"
    );

    public void CreateTheWiseOneDirectory()
    {
        Directory.CreateDirectory(ProjectDirectory);
    }

    public string GetFullFilePath(string fileName)
    {
		return fileName;
        // if (!Directory.Exists(ProjectDirectory))
        // {
        //     CreateTheWiseOneDirectory();
        // }
        // return Path.Combine(ProjectDirectory, fileName);
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

    public dynamic ReadFromFile(string fileName)
    {
        try
        {
            fileName = GetFullFilePath(fileName);
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject(fileContents);
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

    public async void WriteEmptyJSONFile(string fileName)
    {
        try
        {
            Console.WriteLine(ProjectDirectory);
            await File.WriteAllTextAsync(
                GetFullFilePath(fileName),
                JsonConvert.SerializeObject("{}")
            );
        }
        catch (FileNotFoundException) { }
        catch (NullReferenceException) { }
        catch (IOException) { }
        ;
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
