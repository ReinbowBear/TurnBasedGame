using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool PauseFlag;

    public void PauseMethod()
    {
        if (PauseFlag == false)
        {
            BattleKeyboard.gameInput.Disable();
            pausePanel.SetActive(true);

            PauseFlag = true;
            Time.timeScale = 0;
        }
        else
        {
            BattleKeyboard.gameInput.Enable();
            pausePanel.SetActive(false);

            PauseFlag = false;
            Time.timeScale = 1;
        }
    }

    //функции для кнопок в меню
    public void Continue()
    {
        PauseMethod();
    }
    
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        
        SaveSystem.SaveGame();
        Scene.LoadMenu();
    }
}
