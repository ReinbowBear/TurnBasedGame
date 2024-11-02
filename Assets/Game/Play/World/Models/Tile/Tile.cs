using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderMat;
    [SerializeField] private Material[] baseMaterial;
    [Space]
    [SerializeField] private MeshRenderer highlight;
    [SerializeField] private Material[] ActiveMat;

    [HideInInspector] public GameObject isTaken;

    [HideInInspector] public int toStartCost;
    [HideInInspector] public int toTargetCost;
    [HideInInspector] public int fullCost => toStartCost + toTargetCost;
    [HideInInspector] public Tile parent;

    [HideInInspector] public List<Tile> neighbors = new List<Tile>();
    private Vector3[] RayDirection = new Vector3[4];
    [SerializeField] private LayerMask rayLayer;

    void Awake()
    {
        gameObject.name = $"Tile {transform.position.z} {transform.position.x}";

        RayDirection[0] = new Vector3(-1, 0, 0);
        RayDirection[1] = new Vector3(1, 0, 0);
        RayDirection[2] = new Vector3(0, 0, 1);
        RayDirection[3] = new Vector3(0, 0, -1);

        for (byte i = 0; i < RayDirection.Length; i++)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, -0.2f, 0), transform.TransformDirection(RayDirection[i]));

            if (Physics.Raycast(ray, out RaycastHit hit, 1, rayLayer))
            {
                if (hit.transform.gameObject.CompareTag("Tile"))
                {
                    neighbors.Add(hit.transform.GetComponent<Tile>());
                }
            }
        }

        bool isOffset = (transform.position.z % 2 == 0 && transform.position.x % 2 != 0) || (transform.position.z % 2 != 0 && transform.position.x % 2 == 0);
        renderMat.material = isOffset? baseMaterial[1] : baseMaterial[0];
    }

 
    public void ActiveTile() 
    {
        highlight.enabled = true;
        highlight.material = ActiveMat[0];

        gameObject.tag = "ActiveTile";
    }

    public void SweepMat(byte newMat)
    {
        highlight.enabled = true;
        highlight.material = ActiveMat[newMat];
    }

    public void DeactivateTile()
    {
        highlight.enabled = false;
        gameObject.tag = "Tile";
    }


    public int GetDistance(Tile target) 
    {
        Vector3Int dist = new Vector3Int(Mathf.Abs((int)transform.position.x - (int)target.transform.position.x), Mathf.Abs((int)transform.position.z - (int)target.transform.position.z));

        int lowest = Mathf.Min(dist.x, dist.z);
        int highest = Mathf.Max(dist.x, dist.z);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
}
