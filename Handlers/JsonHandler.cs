using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TheWiseOneQuest.Handlers;

public class JsonHandler
{
    public void CreateTheWiseOneDirectory()
    {
        Directory.CreateDirectory(
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TheWiseOneQuest"
            )
        );
    }

    public string GetFullFilePath(string fileName)
    {
        if (
            !Directory.Exists(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "TheWiseOneQuest"
                )
            )
        )
        {
            CreateTheWiseOneDirectory();
        }
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TheWiseOneQuest",
            fileName
        );
    }

    public void CreateFile(string fileName)
    {
        try
        {
            //File.Create(GetFullFilePath(fileName));
            File.Create(fileName);
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
            //fileName = GetFullFilePath(fileName);
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject(fileContents);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }

    public dynamic ReadFromFile<T>(string fileName)
    {
        try
        {
            //fileName = GetFullFilePath(fileName);
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }

    public void WriteToFile<T>(string fileName, string key, T contentToAdd)
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
