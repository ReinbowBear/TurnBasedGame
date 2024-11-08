using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuKeyboard : MonoBehaviour
{
    private GameInput menuInput;

    [SerializeField] private GameObject ChoseObject;
    [SerializeField] private float duration;
    [Space]
    [SerializeField] private Button continueButton;
    [SerializeField] private List<Button> buttons;
    private byte buttonIndex; 

    private Coroutine myCoroutine;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeElapsed;

    void Awake()
    {
        menuInput = new GameInput();

        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            buttons.Insert(0, continueButton);
            continueButton.gameObject.SetActive(true);
        }
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
            if (buttonIndex != buttons.Count-1) //почему то выходит за границы индесов, с -1 работает хорошо
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
            myCoroutine = StartCoroutine(MoveToButton(buttons[buttonIndex].transform.position));
        }
        else
        {
            startPos = ChoseObject.transform.position;
            endPos = buttons[buttonIndex].transform.position;
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
        buttons[buttonIndex].onClick.Invoke();
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
