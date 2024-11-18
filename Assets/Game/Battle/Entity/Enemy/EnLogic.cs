using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnLogic : MonoBehaviour
{
    public Sprite image;
    
    private EnCombat enCombat;
    [HideInInspector] public EnMove enMove;

    private List<PriorityStruct> CostList = new List<PriorityStruct>();
    private byte difficultyLevel = 2;

    void Awake()
    {
        TurnManager.enemys.Add(this);
        //difficultyLevel = PlayerPrefs.

        enCombat = GetComponent<EnCombat>();
        enMove = GetComponent<EnMove>();
    }


    public void CheckMove()
    {
        enMove.PathCheck(CostList);
        enCombat.PriorityCheck(CostList);

        CostList.Sort((priorityClass, otherClass) => priorityClass.fullCost.CompareTo(otherClass.fullCost));
        //int newIndex = enMove.FindCharacters();
        StartCoroutine(DoMove());
    }

    private IEnumerator DoMove()
    {
        yield return StartCoroutine(enMove.MoveTo(CostList[difficultyLevel].tile));

        if (CostList[difficultyLevel].weaponDirection != Vector3.zero)
        {
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(enCombat.StartAttack(CostList[difficultyLevel]));
        }

        CostList.Clear();
    }


    void OnDestroy()
    {
        TurnManager.enemys.Remove(this);
    }
}



public struct PriorityStruct
{
    public Tile tile;
    public Vector3 weaponDirection;
    public int fullCost => player + wall;

    public int player;
    public int wall;
}
