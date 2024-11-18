using DG.Tweening;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform[] Uirect;
    [SerializeField] private Vector2[] hidePos;

    private RectTransform[] ShowPos;

    void Awake()
    {
        ShowPos = new RectTransform[Uirect.Length];

        for (byte i = 0; i < Uirect.Length; i++)
        {
            ShowPos[i] = Uirect[i];
        }
    }


    public void ShowUI()
    {
        for (byte i = 0; i < Uirect.Length; i++)
        {
            Uirect[i].gameObject.SetActive(true);

            Uirect[i].DOAnchorPos(hidePos[i], 0.6f)
                .SetLink(Uirect[i].gameObject)
                .From();
        }
    }

    public void HideUI()
    {
        for (byte i = 0; i < Uirect.Length; i++)
        {
            Uirect[i].gameObject.SetActive(false);
        }
    }


    void OnEnable()
    {
        TurnManager.onEndLevel += HideUI;
    }

    void OnDisable()
    {
        TurnManager.onEndLevel -= HideUI;
    }
}
