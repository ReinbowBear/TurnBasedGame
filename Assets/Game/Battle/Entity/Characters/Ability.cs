using System.Collections.Generic;
using UnityEngine;

public class Ability : Applicable
{    
    [SerializeField] protected byte damage;
    [SerializeField] protected byte maxDist;
    [Space]
    [SerializeField] protected Transform firePos;
    [SerializeField] protected GameObject projectile;
    [Space]
    protected Vector3[] RayDirection = new Vector3[4];
    protected List<Tile> tileList = new List<Tile>();
    [SerializeField] protected LayerMask rayLayer;

    protected virtual void Awake()
    {
        RayDirection[0] = new Vector3(-1, -0, 0);
        RayDirection[1] = new Vector3(1, -0, 0);
        RayDirection[2] = new Vector3(0, -0, 1);
        RayDirection[3] = new Vector3(0, -0, -1);
    }
    

    public override void Prepare()
    {
        for (byte i = 0; i < RayDirection.Length; i++)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, -0.2f, 0), transform.TransformDirection(RayDirection[i]));
            RaycastHit[] rayHits = Physics.RaycastAll(ray, maxDist, rayLayer);
    
            for (byte ii = 0; ii < rayHits.Length; ii++)
            {
                Tile tileScript = rayHits[ii].transform.GetComponent<Tile>();
                tileList.Add(tileScript);
                tileScript.ActiveTile(); 
            }
        }

        DirectionSweepMat(1);
    }

    public override void Activate()
    {
        False();
        
        Projectile projectileScript = Instantiate(projectile, firePos.position, transform.rotation).GetComponent<Projectile>();
        projectileScript.damage = damage;
        projectileScript.spawnPosition = transform.position;
        projectileScript.maxDist = maxDist;
    }


    public override void ChangeDirection(Vector3 direction)
    {
        Vector3 directionToTarget = (direction - transform.position).normalized;
        Vector3 currentForward = transform.forward;

        float angle = Vector3.Angle(currentForward, directionToTarget);

        if (angle > 60) //не поворачиваем персонажа если он уже смотрит куда нужно
        {
            DirectionSweepMat(0);
            transform.LookAt(direction);
            DirectionSweepMat(1);
        }
    }

    protected override void DirectionSweepMat(byte NewMat)
    {
        Ray ray = new Ray(transform.position + new Vector3(0, -0.2f, 0), transform.forward);
        RaycastHit[] rayHits = Physics.RaycastAll(ray, maxDist, rayLayer);

        foreach (RaycastHit hit in rayHits)
        {
            Tile tileScript = hit.transform.GetComponent<Tile>();
            tileScript.SweepMat(NewMat);
        }
    }


    public override void False()
    {
        for (byte i = 0; i < tileList.Count; i++)
        {
            tileList[i].DeactivateTile();
        }
        tileList.Clear();
    }
}
