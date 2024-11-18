using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scene
{
    public static byte currentScene;

    static Scene() //статический конструктор 
    {
        SaveSystem.onSave += Save;
    }


    public static void Continue()
    {
        currentScene = SaveSystem.gameData.saveScene.currentScene;
        SceneManager.LoadScene(currentScene);
    }

    public static void Load(byte sceneIndex)
    {
        currentScene = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


    private static void Save()
    {
        SaveScene saveScene = new SaveScene();
        saveScene.currentScene = currentScene;

        SaveSystem.gameData.saveScene = saveScene;
    }
}

[System.Serializable]
public struct SaveScene
{
    public byte currentScene;
}
