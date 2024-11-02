using UnityEngine;

public class piercing : Projectile
{
    [SerializeField] private byte piercingCount;
    private byte currentPiercing = 0;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health abstractHP))
        {
            abstractHP.TakeDamage(damage);

            if (currentPiercing == piercingCount)
            {
                Destroy(gameObject);
            }
            
            currentPiercing++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
