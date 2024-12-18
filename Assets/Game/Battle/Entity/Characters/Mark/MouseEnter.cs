using UnityEngine;

public class MouseEnter : MonoBehaviour
{
    [SerializeField] private RotateObject rotateObject;
    [SerializeField] private LogicCharacter logicCharacter;

    void OnMouseEnter()
    {
        logicCharacter.hpBar.SetActive(true);
        if (logicCharacter.combatCharacter.wasAttaking != true || logicCharacter.moveCharacter.wasMoved != true)
        {
            if (logicCharacter.moveCharacter.isPathCheck == false || logicCharacter.combatCharacter.isAttacking == true)
            {
                rotateObject.enabled = true;
            }
        }
    }

    void OnMouseExit()
    {
        rotateObject.enabled = false;
        if (logicCharacter.moveCharacter.isPathCheck == false && logicCharacter.combatCharacter.isAttacking == false)
        {
            logicCharacter.hpBar.SetActive(false);
        }
    }
}
