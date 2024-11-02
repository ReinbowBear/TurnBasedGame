using UnityEngine;

public class Finish : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishTriger"))
        {
            Debug.Log("победа!");
        }
    }
}
