using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private MenuKeyboard menuKeyboard;


    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        menuKeyboard.panels.Add(this);
    }

    void OnDisable()
    {
        menuKeyboard.panels.Remove(this);
    }
}
