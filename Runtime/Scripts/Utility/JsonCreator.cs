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
    public static PackageCreatorEditorTool.Package LoadJson( string jsonPath)
    {
        string path = jsonPath + "/package.json";
        PackageCreatorEditorTool.Package tempPackage=new PackageCreatorEditorTool.Package();
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            tempPackage=JsonUtility.FromJson<PackageCreatorEditorTool.Package>(jsonString);
            Debug.Log(tempPackage.name);
            Debug.Log(tempPackage.displayName);
            Debug.Log(jsonString);
        }
        else
        {
            Debug.Log("No file.");
        }
        return tempPackage;
    }
}
