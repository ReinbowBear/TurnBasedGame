using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private MenuKeyboard menuKeyboard;
    [SerializeField] private byte buttonId;

    public void OnPointerEnter(PointerEventData pointerEventData) 
    { 
        menuKeyboard.MoveTo(buttonId);
    } 
}
