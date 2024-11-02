using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCombat : MonoBehaviour
{
    
    [SerializeField] private byte damage;
    [SerializeField] private byte maxDist;
    [Space]
    [SerializeField] private GameObject model;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectile;
    [SerializeField] private LayerMask rayLayer;

    private Vector3[] RayDirection = new Vector3[4];

    [SerializeField] private byte playerCost;
    [SerializeField] private byte wallCost;
    
    void Awake()
    {
        RayDirection[0] = new Vector3(-1, 0, 0);
        RayDirection[1] = new Vector3(1, 0, 0);
        RayDirection[2] = new Vector3(0, 0, 1);
        RayDirection[3] = new Vector3(0, 0, -1);
    }


    public void PriorityCheck(List<PriorityStruct> costList)
    {
        for (byte i = 0; i < costList.Count; i++)
        {
            for (byte ii = 0; ii < RayDirection.Length; ii++)
            {
                Ray ray = new Ray(costList[i].tile.transform.position + new Vector3(0, firePos.position.y, 0), transform.TransformDirection(RayDirection[ii]));
                if (Physics.Raycast(ray, out RaycastHit hit, maxDist))
                {
                    if (hit.transform.CompareTag("Wall"))
                    {
                        PriorityStruct priorityData = costList[i];
                        priorityData.wall -= wallCost;
                        costList[i] = priorityData;
                    }
                    if (hit.transform.CompareTag("Player"))
                    {
                        PriorityStruct priorityData = costList[i];
                        priorityData.player -= playerCost;
                        costList[i] = priorityData;

                        //AbstractHP abstractHP = hit.transform.GetComponent<AbstractHP>(); //возможно стоит взять хп игрока и отнять приоритет, враг игнорит персонажей с прорити менее 25
                        //costList[i].player += Mathf.RoundToInt(abstractHP.currentHealth); //у игрока может быть так много хп что он перестанет быть приоритетной целью

                        float distance = Vector3.Distance(costList[i].tile.transform.position, hit.transform.position);
                        distance = maxDist - distance;
                        
                        priorityData = costList[i];
                        priorityData.player += Mathf.RoundToInt(distance);
                        priorityData.weaponDirection = hit.transform.position;

                        costList[i] = priorityData;
                    }
                }
            }
        }
    }


    public IEnumerator StartAttack(PriorityStruct costList)
    {
        model.transform.LookAt(costList.weaponDirection);

        Ray ray = new Ray(transform.position + new Vector3(0, -0.2f, 0), model.transform.forward);
        RaycastHit[] rayHits = Physics.RaycastAll(ray, maxDist, rayLayer);

        for(byte i = 0; i < rayHits.Length; i++)
        {
            Tile tileScript = rayHits[i].transform.GetComponent<Tile>();
            tileScript.SweepMat(1);
        }

        yield return new WaitForSeconds(0.2f);

        Projectile projectileScript = Instantiate(projectile, firePos.position, model.transform.rotation).GetComponent<Projectile>();
        projectileScript.damage = damage;
        projectileScript.spawnPosition = transform.position;
        projectileScript.maxDist = maxDist;

        for(byte i = 0; i < rayHits.Length; i++)
        {
            Tile tileScript = rayHits[i].transform.GetComponent<Tile>();
            tileScript.DeactivateTile();
        }
    }
}
