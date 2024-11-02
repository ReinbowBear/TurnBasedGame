using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    private Image splashScreen;
    void Awake()
    {
        splashScreen = GetComponent<Image>();
        splashScreen.enabled = true;
    }

    void Start()
    {
        splashScreen.DOFade(0, 1)
        .SetDelay(0.4f)
        .SetLink(splashScreen.rectTransform.gameObject)
        .OnComplete(() => { splashScreen.enabled = false; });
    }
}
