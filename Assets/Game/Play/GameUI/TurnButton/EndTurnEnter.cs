using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurnEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action OnTurnButtonEnter;

    public void OnPointerEnter(PointerEventData pointerEventData) 
    { 
        OnTurnButtonEnter?.Invoke();
    } 

    public void OnPointerExit(PointerEventData pointerEventData) 
    { 
        OnTurnButtonEnter?.Invoke();
    } 
}
