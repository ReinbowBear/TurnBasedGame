using UnityEngine;

public class HpBarRotate : MonoBehaviour
{
    private Vector3 direction;
    
    void Awake()
    {
        direction = Camera.main.transform.forward;
    }

    void LateUpdate()
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = newRotation;
    }
}
