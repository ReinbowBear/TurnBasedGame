using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject characterMenu;

    public void OpenGame()
    {
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
}
