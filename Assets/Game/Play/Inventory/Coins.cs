using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private byte maxCoins;
    private byte currentCoins;
    [Space]
    [SerializeField] private TextMeshProUGUI coinHud;


    public void StartCoins()
    {
        currentCoins = maxCoins;
        RefreshCoinCount();
    }

    public void UseCoins(byte usedCount)
    {
        if (currentCoins >= usedCount)
        {
            currentCoins -= usedCount;
            RefreshCoinCount();
        }
        else
        {
            Debug.Log("недостаточно монет!");
        }
    }


    private void RefreshCoinCount() //можно приписать различные эффекты при потере монет
    {
        coinHud.text = currentCoins.ToString();
    }


    void OnEnable()
    {
        //подписка на событие нового уровня или типа того
    }

    void OnDisable()
    {

    }
}
