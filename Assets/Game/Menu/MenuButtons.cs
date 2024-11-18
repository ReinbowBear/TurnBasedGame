using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    

    public void ContinueGame()
    {
        Scene.Continue();
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        Scene.Load(1);
    }


    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }


    public void ExitGame()
    {
        Debug.Log("Отсюда нет выхода.. x_x");
        Application.Quit();
    }
}
