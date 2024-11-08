using System;
using System.IO;
using UnityEngine;

public static class SaveSystem //https://www.youtube.com/watch?v=1mf730eb5Wo&t=417s&ab_channel=SasquatchBStudios
{
    public static Action onSave;
    public static Action onLoad;
    
    public static GameData gameData = new GameData();

    public static void SaveGame()
    {
        File.WriteAllText(GetFileName(), JsonUtility.ToJson(gameData, true));
    }

    public static void LoadGame()
    {
        string saveContent = File.ReadAllText(GetFileName());
        gameData = JsonUtility.FromJson<GameData>(saveContent);

        onLoad.Invoke();
    }

    public static void DeleteSave()
    {
        string file = GetFileName();
        
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }


    public static string GetFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }
}


[System.Serializable] //сериализация позволяет записывать данные в файл
public struct GameData
{
    public SaveInventoryContent saveInventoryContent;
    public SaveGlobalMap saveGlobalMap;
}
