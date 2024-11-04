using UnityEngine;
using UnityEngine.UI;

public class CharacterHud : MonoBehaviour
{
    [HideInInspector] public GameObject myCharacter;

    [SerializeField] private Image AttackStatus;
    [SerializeField] private Image MoveStatus;
    [SerializeField] private Image deadIcone;

    private void RefreshStatus()
    {
        AttackStatus.enabled = true;
        MoveStatus.enabled = true;
    }

    private void OnAttack(GameObject character)       
    {
        if (character == myCharacter)
        {
            AttackStatus.enabled = false;
        }
    }

    private void OnMove(GameObject character)
    {
        if (character == myCharacter)
        {
            MoveStatus.enabled = false;
        }
    }

    private void CharacterDead(GameObject deadCharacter)
    {
        if (deadCharacter == myCharacter)
        {
            AttackStatus.enabled = false;
            MoveStatus.enabled = false;
            deadIcone.enabled = true;
            OnDisable();
        }
    }


    void OnEnable()
    {
        TurnManager.onPlayerTurn += RefreshStatus;

        CombatCharacter.onAttack += OnAttack;
        MoveCharacter.onMove += OnMove;

        CharacterHP.onDead += CharacterDead;
    }

    void OnDisable()
    {
        TurnManager.onPlayerTurn -= RefreshStatus;

        CombatCharacter.onAttack -= OnAttack;
        MoveCharacter.onMove -= OnMove;

        CharacterHP.onDead -= CharacterDead;
    }
}
