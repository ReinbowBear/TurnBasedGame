using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManna : MonoBehaviour
{
    [SerializeField] private byte maxManna;
    [SerializeField] private byte startManna;
    [Space]
    [SerializeField] private GameObject mannaPoint;
    [SerializeField] private TextMeshProUGUI mannaText;

    private Image[] mannaBar;
    private int mannaId;
    private byte MannaPointsCount;

    void Awake()
    {
        mannaText.text = startManna.ToString();
        mannaId = startManna-1;

        mannaBar = new Image[maxManna];
        for (byte i = 0; i < maxManna; i++)
        {
            mannaBar[i] = Instantiate(mannaPoint, transform).GetComponent<Image>();
        }

        for (byte i = 0; i < maxManna; i++)
        {
            if (i > startManna-1)
            {
                mannaBar[i].rectTransform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                mannaBar[i].enabled = true;
                MannaPointsCount++;
            }
        }
    }


    private void NewManna()
    {
        if (mannaId < mannaBar.Length-1)
        {
            mannaId++;
            mannaBar[mannaId].enabled = true;

            MannaPointsCount++;
            mannaText.text = MannaPointsCount.ToString();

            DOTween.Sequence()
            .SetLink(mannaBar[mannaId].rectTransform.gameObject)
            .Append(mannaBar[mannaId].rectTransform.DOScale(1.1f, 0.4f))
            .Append(mannaBar[mannaId].rectTransform.DOScale(1, 0.2f));
        }
        else
        {
            for (byte i = 0; i < mannaBar.Length; i++)
            {
                float wait = 0.1f * i;

                DOTween.Sequence()
                .AppendInterval(wait)
                .SetLink(mannaBar[i].rectTransform.gameObject)
                .Append(mannaBar[i].rectTransform.DOScale(1.1f, 0.2f))
                .Append(mannaBar[i].rectTransform.DOScale(1, 0.2f));
            }
        }
    }

    private void UseManna(byte usedCount) //функция протестирована и работает, можно юзать будет
    {
        if (MannaPointsCount >= usedCount)
        {
            DOTween.Sequence()
            .SetLink(mannaBar[mannaId].rectTransform.gameObject)
            .Append(mannaBar[mannaId].rectTransform.DOScale(1.1f, 0.2f))
            .Append(mannaBar[mannaId].rectTransform.DOScale(0, 0.4f))
            .OnComplete(() => UseMannaComplete(usedCount)); //нужно запустить разок функцию что зафолсить поинты

            mannaId--;

            for (int i = 1; i < usedCount; i++)
            {
                DOTween.Sequence()
                .SetLink(mannaBar[mannaId].rectTransform.gameObject)
                .Append(mannaBar[mannaId].rectTransform.DOScale(1.1f, 0.2f))
                .Append(mannaBar[mannaId].rectTransform.DOScale(0, 0.4f));

                mannaId--;
            }

            MannaPointsCount -= usedCount;
            mannaText.text = MannaPointsCount.ToString();
        }
        else
        {
            for (byte i = 0; i < MannaPointsCount; i++)
            {
                DOTween.Sequence()
                .SetLink(mannaBar[i].rectTransform.gameObject)
                .Append(mannaBar[i].rectTransform.DOScale(0.8f, 0.2f))
                .Append(mannaBar[i].rectTransform.DOScale(1, 0.2f));
            }
        }
    }

    private void UseMannaComplete(byte usedManna)
    {
        for (int i = MannaPointsCount; i < MannaPointsCount + usedManna; i++)
        {
            mannaBar[i].enabled = false;
        }
    }


    void OnEnable()
    {
        TurnManager.onPlayerTurn += NewManna;
    }

    void OnDisable()
    {
        TurnManager.onPlayerTurn -= NewManna;
    }
}
