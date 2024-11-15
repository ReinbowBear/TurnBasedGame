using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [HideInInspector] public float currentHealth;
    [Space]
    [SerializeField] protected HpBar hpBar;
    [SerializeField] protected LayerMask rayLayer;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        hpBar.HpBarChange(maxHealth, currentHealth);

        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1, rayLayer))
        {
            hit.transform.GetComponent<Tile>().isTaken = gameObject;
        }
    }


    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.HpBarChange(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void TakeHeal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        hpBar.HpBarChange(maxHealth, currentHealth);
    }

    protected virtual void Death()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1, rayLayer))
        {
            hit.transform.GetComponent<Tile>().isTaken = null;
        }
        Destroy(gameObject);
    }

    public IEnumerator PushAway(Vector3 targetPos) //старая функция с времён 0.2, решил что может пригодится
    {
        targetPos = transform.position + targetPos;
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 10 * Time.deltaTime);
            yield return null;
        }
    }
}
