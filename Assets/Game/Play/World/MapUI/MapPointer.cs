using UnityEngine;

public class MapPointer : MonoBehaviour
{
    private RectTransform pointer;

    void Awake()
    {
        pointer = GetComponent<RectTransform>();
    }

    public void SetTarget(RectTransform start, RectTransform end)
    {
        Vector2 startPos = start.anchoredPosition;
        Vector2 endPos = end.anchoredPosition;

        pointer.anchoredPosition = endPos - startPos;
        pointer.anchoredPosition = startPos + pointer.anchoredPosition/2;

        Vector2 direction = endPos - startPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pointer.rotation = Quaternion.Euler(0, 0, angle);

        float distance = direction.magnitude;
        pointer.sizeDelta = new Vector2(distance, pointer.sizeDelta.y)/2;
    }
}
