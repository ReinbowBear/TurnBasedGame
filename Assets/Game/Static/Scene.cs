using UnityEngine.SceneManagement;

public static class Scene
{
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
