
public class Helmet : Equipment
{
    protected override void OnEnable()
    {
        Health hpScript = transform.root.GetComponent<Health>();
        hpScript.maxHealth = hpScript.maxHealth + value;
    }

    protected override void OnDisable()
    {
        Health hpScript = transform.root.GetComponent<Health>();
        hpScript.maxHealth = hpScript.maxHealth - value;
    }
}
