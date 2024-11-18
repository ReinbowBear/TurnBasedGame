using UnityEngine;
using UnityEngine.InputSystem;

public class MapKeyboard : MonoBehaviour
{
    private GameInput mapKeyboard;

    void Awake()
    {
        mapKeyboard = new GameInput();
    }


    private void ExitToMenu(InputAction.CallbackContext context)
    {
        Scene.LoadMenu();
    }


    void OnEnable()
    {
        mapKeyboard.Enable();

        mapKeyboard.Menu.Esc.started += ExitToMenu;
    }

    void OnDisable()
    {
        mapKeyboard.Menu.Esc.started -= ExitToMenu;

        mapKeyboard.Disable();
    }
}
