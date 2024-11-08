using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool PauseFlag;
    private GameInput myInput;

    void Awake()
    {
        myInput = new GameInput();
    }


    private void PauseMethod(InputAction.CallbackContext context)
    {
        if (PauseFlag == false)
        {
            GameKeyboard.gameInput.Disable();
            myInput.Enable();

            pauseMenu.SetActive(true);

            PauseFlag = true;
            Time.timeScale = 0;
        }
        else
        {
            GameKeyboard.gameInput.Enable();
            myInput.Disable();

            pauseMenu.SetActive(false);

            PauseFlag = false;
            Time.timeScale = 1;
        }
    }


    void Start() //тут должен был быть OnEnable, но почему то нулевой референс возникает тут и у CamControl
    {
        GameKeyboard.gameInput.Player.Esc.started += PauseMethod;
        myInput.Player.Esc.started += PauseMethod;
    }

    void OnDestroy()
    {
        GameKeyboard.gameInput.Player.Esc.started -= PauseMethod;
        myInput.Player.Esc.started -= PauseMethod;
    }


    //функции для кнопок в меню
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
