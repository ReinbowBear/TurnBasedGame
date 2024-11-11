using UnityEngine;

public class GameKeyboard : MonoBehaviour
{
    public static GameInput gameInput;

    void Awake()
    {
        gameInput = new GameInput();
    }


    void OnEnable()
    {
        gameInput.Enable();
    }

    void OnDisable()
    {
        gameInput.Disable();
    }
}
