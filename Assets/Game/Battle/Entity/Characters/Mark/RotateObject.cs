using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private byte rotationSpeed;

    void Awake()
    {
        rotationSpeed = 30;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void LateUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }


    void OnEnable()
    {
        meshRenderer.enabled = true;
    }

    void OnDisable()
    {
        meshRenderer.enabled = false;
    }
}
