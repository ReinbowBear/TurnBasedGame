using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [HideInInspector] public short damage;
    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public byte maxDist;

    protected byte moveSpeed = 20;
    protected Rigidbody rb;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void LateUpdate()
    {
        rb.velocity = transform.forward * moveSpeed;

        float distanceTravelled = Vector3.Distance(spawnPosition, transform.position);
        if (distanceTravelled > maxDist)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health abstractHP))
        {
            abstractHP.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
