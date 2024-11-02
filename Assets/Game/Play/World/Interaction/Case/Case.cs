using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private RotateObject rotateScript;
    [SerializeField] private BoxCollider colider;

    private void DropCase(GameObject deadCharacter)
    {
        if (deadCharacter == transform.parent.gameObject)
        {
            Case newCase = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Case>();
            newCase.rotateScript.enabled = true;
            newCase.colider.enabled = true;

            CharacterHP.onDead -= DropCase;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.SetParent(other.transform);
            transform.localPosition = new Vector3(0, 0, 0);
            rotateScript.enabled = false;
            colider.enabled = false;

            CharacterHP.onDead += DropCase;
        }
    }
}
