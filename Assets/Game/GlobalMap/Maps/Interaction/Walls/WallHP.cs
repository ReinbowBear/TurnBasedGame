using UnityEngine;

public class WallHP : Health
{
    [Space]
    [SerializeField] private Mesh damagedWall;
    private MeshFilter meshFilter;

    protected override void Awake()
    {
        currentHealth = maxHealth;
        meshFilter = GetComponentInChildren<MeshFilter>();

        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1, rayLayer))
        {
            hit.transform.GetComponent<Tile>().isTaken = gameObject;
        }
    }


    public override void TakeDamage(int damage)
    {
        damage = 1;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            base.Death();
        }
        else
        {
            meshFilter.mesh = damagedWall;
        }
    }
}
