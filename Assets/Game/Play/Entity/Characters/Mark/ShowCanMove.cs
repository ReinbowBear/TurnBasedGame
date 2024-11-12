using UnityEngine;

public class ShowCanMove : MonoBehaviour
{
    [SerializeField] private CombatCharacter abilityCharacter;
    [SerializeField] private MoveCharacter moveCharacter;
    [Space]
    [SerializeField] private MeshRenderer attack;
    [SerializeField] private MeshRenderer move;
    private bool active;


    private void CanHaveTurn()
    {
        if (active == false)
        {
            active = true;
            if (abilityCharacter.wasAttaking != true)
            {
                attack.enabled = true;
            }

            if (moveCharacter.wasMoved != true)
            {
                move.enabled = true;
            }
        }
        else
        {
            active = false;
            attack.enabled = false;
            move.enabled = false;
        }
    }


    void OnEnable()
    {
        EndTurnEnter.OnTurnButtonEnter += CanHaveTurn;
    }

    void OnDisable()
    {
        EndTurnEnter.OnTurnButtonEnter -= CanHaveTurn;
    }
}
