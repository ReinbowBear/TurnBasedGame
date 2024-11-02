using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHud : MonoBehaviour
{
    [SerializeField] private Image[] slotsIcone;
    [SerializeField] private Image[] useIcone;
    [Space]
    [SerializeField] private TextMeshProUGUI mannaCount;

    public void ChoseAbility(int id) //это для button на UI
    {
        ClickCharacter.choseCharacter.ChoseAttack(id);
    }

    private void RefreshIcon()       
    {
        CombatCharacter ability = ClickCharacter.choseCharacter.combatCharacter;

        slotsIcone[0].sprite = ability.myWeapon.icone;

        if (ability.myAbility != null)
        {
            slotsIcone[1].sprite = ability.myAbility.icone;
            mannaCount.text = ability.mannaCount.ToString();
        }
        else
        {
            slotsIcone[1].sprite = null;
            mannaCount.text = 0.ToString();
        }
        RefreshUseIcone(gameObject);
    }

    private void RefreshUseIcone(GameObject gameObject) //этой функции не нужен аргумент, но он передается в событии
    {
        CombatCharacter ability = ClickCharacter.choseCharacter.combatCharacter;
        if (ability.wasAttaking == true)
        {
            for (byte i = 0; i < useIcone.Length; i++)
            {
                useIcone[i].enabled = true;
            }
        }
        else
        {
            for (byte i = 0; i < useIcone.Length; i++)
            {
                useIcone[i].enabled = false;
            }
        }
    }


    void OnEnable()
    {
        ClickCharacter.onChoiceCharacter += RefreshIcon;
        CombatCharacter.onAttack += RefreshUseIcone;
    }

    void OnDisable()
    {
        ClickCharacter.onChoiceCharacter -= RefreshIcon;
        CombatCharacter.onAttack -= RefreshUseIcone;
    }
}
