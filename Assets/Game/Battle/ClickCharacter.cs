using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickCharacter : MonoBehaviour
{
    public static Action onChoiceCharacter;
    private Camera cam;

    public static LogicCharacter choseCharacter;
    [SerializeField] private LayerMask rayLayer;

    void Awake()
    {
        cam = Camera.main;
        //rayLayer = LayerMask.GetMask("Tile"); //кажется это довольно ненадёжно если я что то поменяю, но хотелось бы помнить как выглядит запись
    }


    private void ClickSomeone(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer) && EventSystem.current.IsPointerOverGameObject() != true) 
        {            
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile.isTaken != null && tile.isTaken.CompareTag("Player"))
            {
                DropCharacter(context);
                choseCharacter = tile.isTaken.GetComponent<LogicCharacter>();
                choseCharacter.ChoseCharacter();
                onChoiceCharacter.Invoke();
            }
            //else if (tile.isTaken != null && tile.isTaken.CompareTag("Enemy")) //тут должна быть функция что показывает дальность хотьбы врага и всё такое
            {
                //EnLogic enLogic = tile.isTaken.GetComponent<EnLogic>();
                //enLogic.enMove. 
            }
        }
    }

    public void ChooseAbility(int InputKey)
    {
        if (choseCharacter != null)
        {
            choseCharacter.ChoseAttack(InputKey);
        }
    }

    private void DropCharacter(InputAction.CallbackContext context)
    {
        if (choseCharacter != null)
        {
            choseCharacter.FalseCharacter();
        }
    }


    void OnEnable()
    {
        BattleKeyboard.gameInput.Player.Mouse_0.started += ClickSomeone;
        BattleKeyboard.gameInput.Player.Mouse_1.started += DropCharacter;
        
        BattleKeyboard.gameInput.Player.Slot_1.started += context => ChooseAbility(0);
        BattleKeyboard.gameInput.Player.Slot_2.started += context => ChooseAbility(1);
    }

    void OnDisable()
    {
        BattleKeyboard.gameInput.Player.Mouse_0.started -= ClickSomeone;
        BattleKeyboard.gameInput.Player.Mouse_1.started -= DropCharacter;

        BattleKeyboard.gameInput.Player.Slot_1.started -= context => ChooseAbility(0);
        BattleKeyboard.gameInput.Player.Slot_2.started -= context => ChooseAbility(1);
    }
}
