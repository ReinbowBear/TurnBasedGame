using UnityEngine;

public class LogicCharacter : MonoBehaviour
{
    public GameObject hpBar;
    [HideInInspector] public CombatCharacter combatCharacter;
    [HideInInspector] public MoveCharacter moveCharacter;
    private int activeWeapon;

    void Awake()
    {
        combatCharacter = GetComponent<CombatCharacter>();
        moveCharacter = GetComponent<MoveCharacter>();
    }


    public void ChoseCharacter()
    {
        hpBar.SetActive(true);
        
        if (moveCharacter.isPathCheck != true && moveCharacter.wasMoved != true)
        {
            moveCharacter.PathCheck();
            activeWeapon = 10;
        }
    }

    public void FalseCharacter()
    {
        combatCharacter.FalseAttack();
        moveCharacter.FalsePath();
        activeWeapon = 10;
        hpBar.SetActive(false);
    }


    public void ChoseAttack(int InputKey)
    {
        if (combatCharacter.wasAttaking != true && moveCharacter.isMove != true && activeWeapon != InputKey)
        {
            FalseCharacter();
            activeWeapon = InputKey;
            combatCharacter.CharacterAttack(InputKey);
            hpBar.SetActive(true);
        }
        else
        {
            combatCharacter.FalseAttack();
            ChoseCharacter();
        }
    }


    private void NewTurn()
    {
        combatCharacter.wasAttaking = false;
        moveCharacter.wasMoved = false;
    }


    void OnEnable()
    {
        TurnManager.onPlayerTurn += NewTurn;
    }

    void OnDisable()
    {
        TurnManager.onPlayerTurn -= NewTurn;
    }
}
