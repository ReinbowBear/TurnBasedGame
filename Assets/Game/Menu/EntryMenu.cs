using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EntryMenu : MonoBehaviour
{
    [SerializeField] private Image splashScreen;
    [SerializeField] private MenuKeyboard menuKeyboard;

    void Start()
    {
        CheckSave();
        blackScreen();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            menuKeyboard.buttons[0].gameObject.SetActive(true);
            menuKeyboard.MoveTo(0);

            SaveSystem.LoadFile();
        }
    }

    private void blackScreen()
    {
        splashScreen.enabled = true;

        splashScreen.DOFade(0, 1)
        .SetDelay(0.4f)
        .SetLink(splashScreen.rectTransform.gameObject)
        .OnComplete(() => { splashScreen.enabled = false; });
    }
}
