using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditorInternal;

public static class JsonCreator 
{
    public static void SaveJson(PackageCreatorEditorTool.Package jsonContent, string jsonPath)
    {
        string path = jsonPath + "/package.json";
        if (File.Exists(path))
        {
            string jsonString = JsonUtility.ToJson(jsonContent);
            File.WriteAllText(path, jsonString);
        }
        else
        {
            string jsonString = JsonUtility.ToJson(jsonContent);
            File.WriteAllText(path, jsonString);

        }
    }
    public static void SaveAssembly(PackageCreatorEditorTool.AssemblyDefination assemblyDefination, string jsonPath)
    {
        string path = jsonPath + "/"+assemblyDefination.name+".asmdef";
        DeleteFiles(jsonPath);
        if (File.Exists(path))
        {
            string jsonString = JsonUtility.ToJson(assemblyDefination);
            File.WriteAllText(path, jsonString);
        }
        else
        {
            string jsonString = JsonUtility.ToJson(assemblyDefination);
            File.WriteAllText(path, jsonString);
        }
    }
    public static void DeleteFiles(string path)
    {
        string[] filePaths = Directory.GetFiles(path + "/", "*.asmdef");
        foreach(string file in filePaths)
        {
            File.Delete(file);
        }
    }
    public static void LoadJson(PackageCreatorEditorTool.Package jsonContent, string jsonPath)
    {
        string path = jsonPath + "/package.json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonString, jsonContent);
        }
        else
        {
            Debug.Log("No file.");
        }
    }
}
