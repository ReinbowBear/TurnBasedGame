using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject characterMenu;

    public void ContinueGame()
    {
        SceneManager.LoadScene("Play");
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        SceneManager.LoadScene("Play");
    }


    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }


    public void OpenCharacterMenu()
    {
        characterMenu.SetActive(true);
    }
    public void CloseCharacterMenu()
    {
        characterMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Отсюда нет выхода.. О_О");
        Application.Quit();
    }
}
