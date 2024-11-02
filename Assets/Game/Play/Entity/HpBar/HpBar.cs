using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private TextMeshProUGUI hpText;
    
    public void HpBarChange(int maxHealth, float currentHealth)
    {
        hpText.text = currentHealth.ToString();

        Color newColor = hpImage.color; // Поле color у компонента Image — это структура, и вы не можете изменить его поля напрямую
        newColor.a = currentHealth / maxHealth;
        hpImage.color = newColor;
    }
}
