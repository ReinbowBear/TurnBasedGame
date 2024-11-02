using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuKeyboard : MonoBehaviour
{
    private GameInput menuInput;

    [SerializeField] private GameObject ChoseObject;
    [SerializeField] private float duration;
    [SerializeField] private GameObject[] Buttons;
    private byte buttonIndex; 

    private Coroutine myCoroutine;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeElapsed;

    void Awake()
    {
        menuInput = new GameInput();
    }

    private void NewButton(InputAction.CallbackContext context)
    {
        float value = menuInput.Menu.Navigate.ReadValue<float>();

        if (value == 1)
        {
            if (buttonIndex != 0)
            {
                buttonIndex--;
            }
        }
        else if (value == -1)
        {
            if (buttonIndex != Buttons.Length-1) //почему то выходит за границы массива, с -1 работает хорошо
            {
                buttonIndex++;
            }
        }

        CheckCoroutine(buttonIndex);
    }

    public void CheckCoroutine(byte newIndex)
    {
        buttonIndex = newIndex;
        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(MoveToButton(Buttons[buttonIndex].transform.position));
        }
        else
        {
            startPos = ChoseObject.transform.position;
            endPos = Buttons[buttonIndex].transform.position;
            timeElapsed = 0f;
        }
    }

    private IEnumerator MoveToButton(Vector3 targetPos)
    {
        startPos = ChoseObject.transform.position;
        endPos = targetPos;
   
        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            ChoseObject.transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        ChoseObject.transform.position = endPos;
        myCoroutine = null;
    }

    private void EnterOnButton(InputAction.CallbackContext context)
    {
        Button button = Buttons[buttonIndex].GetComponent<Button>();

        button.onClick.Invoke();
    }

    private void ExitPanel(InputAction.CallbackContext context)
    {
        Debug.Log("реализовать выход по нажатию Esc, если панели к тому времени ещё существуют");
    }


    void OnEnable()
    {
        menuInput.Enable();
        
        menuInput.Menu.Submit.started += EnterOnButton;
        menuInput.Menu.Navigate.started += NewButton;
        menuInput.Menu.Esc.started += ExitPanel;
    }

    void OnDisable()
    {
        menuInput.Menu.Submit.started -= EnterOnButton;
        menuInput.Menu.Navigate.started -= NewButton;
        menuInput.Menu.Esc.started -= ExitPanel;

        menuInput.Disable();
    }
}
