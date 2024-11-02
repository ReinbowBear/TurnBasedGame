using UnityEngine;

public class ShowCanMove : MonoBehaviour
{
    private CombatCharacter abilityCharacter;
    private MoveCharacter moveCharacter;
    
    [SerializeField] private MeshRenderer attack;
    [SerializeField] private MeshRenderer move;
    private bool active;

    void Awake()
    {
        abilityCharacter = transform.root.GetComponent<CombatCharacter>();
        moveCharacter = transform.root.GetComponent<MoveCharacter>();
    }


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
