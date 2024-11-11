using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static Action onPlayerTurn;
    public static Action onEndLevel;

    public static List<Weapon> characterAttacks = new List<Weapon>();
    //public static List<EnemyWeapon> enemyAttacks = new List<EnemyWeapon>();

    public static List<EnLogic> enemys = new List<EnLogic>();
    
    private bool isEnemyTurn;


    public void StartEnemyTurn()
    {        
        if (isEnemyTurn == false)
        {
            if (characterAttacks.Count != 0 || enemys.Count != 0)
            {
                StartCoroutine(EnemyTurn());
            }
            else
            {
                onEndLevel.Invoke();
            }
        }
    }

    private IEnumerator EnemyTurn()
    {
        isEnemyTurn = true;
        GameKeyboard.gameInput.Disable();

        if (characterAttacks.Count != 0)
        {
            yield return new WaitForSeconds(0.2f);
            for (byte i = 0; i < characterAttacks.Count; i++)
            {
                characterAttacks[i].Activate();
                yield return new WaitForSeconds(0.2f);
            }

            characterAttacks.Clear();
        }

        if (enemys.Count != 0)
        {
            for (byte i = 0; i < enemys.Count; i++)
            {
                enemys[i].CheckMove();
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.4f);
        }

        isEnemyTurn = false;
        
        GameKeyboard.gameInput.Enable();

        SaveSystem.onSave.Invoke();
        onPlayerTurn.Invoke();
    }
}
