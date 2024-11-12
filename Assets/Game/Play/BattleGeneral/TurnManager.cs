using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static Action onPlayerTurn;
    public static Action onEndLevel;

    [SerializeField] private byte maxTurn;
    private byte turnTime;

    public static List<Weapon> characterAttacks = new List<Weapon>();
    //public static List<EnemyWeapon> enemyAttacks = new List<EnemyWeapon>();

    public static List<EnLogic> enemys = new List<EnLogic>();
    
    private bool isEnemyTurn;

    void Awake()
    {
        turnTime = maxTurn;
    }


    public void StartEnemyTurn()
    {        
        if (isEnemyTurn == false)
        {
            turnTime--;
            StartCoroutine(EnemyTurn());
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

        CheckBattle();
    }


    private void CheckBattle()
    {
        if (turnTime == 0 || GetCharacter.characterList.Count == 0)
        {
            turnTime = maxTurn;
            onEndLevel.Invoke();
        }
        else
        {
            onPlayerTurn.Invoke();
        }
    }
}
