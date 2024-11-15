using UnityEngine;

public abstract class Equipment : MonoBehaviour
{    
    [SerializeField] protected byte value;

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {
        
    }
}
