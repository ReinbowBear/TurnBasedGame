using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuKeyboard : MonoBehaviour
{
    private GameInput menuInput;

    [SerializeField] private GameObject ChoseObject;
    [SerializeField] private float duration;
    [Space]
    public Button[] buttons;
    [Space]
    [SerializeField] private Pause pause;

    [HideInInspector] public List<Panel> panels = new List<Panel>();

    private Coroutine myCoroutine;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeElapsed;

    private int buttonIndex; 

    void Awake()
    {
        menuInput = new GameInput();
    }


    private void NewButton(InputAction.CallbackContext context)
    {
        float value = menuInput.Menu.Navigate.ReadValue<float>();

        if (value == 1)
        {
            buttonIndex--;
        }
        else
        {
            buttonIndex++;
        }

        buttonIndex = Mathf.Clamp(buttonIndex, 0, buttons.Length-1);
        if (buttons[buttonIndex].gameObject.activeSelf == true) //проверка на активность для кнопки продолжить игру.. всё чутка костыльно да
        {
            MoveTo(buttonIndex);
        }
    }

    public void MoveTo(int newIndex)
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
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        ChoseObject.transform.position = endPos;
        myCoroutine = null;
    }


    private void EnterOnButton(InputAction.CallbackContext context)
    {
        if (buttons[buttonIndex].transform.root.gameObject.activeSelf == true) //можно было активировать кнопку с выключенной менюшкой лол
        {
            buttons[buttonIndex].onClick.Invoke();
        }
    }

    private void ExitPanel(InputAction.CallbackContext context)
    {
        if (panels.Count != 0)
        {
            panels[panels.Count-1].gameObject.SetActive(false); //панель сама себя уберёт так же как добавила
        }
        else if (pause != null)
        {
            pause.PauseMethod();
        }
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
