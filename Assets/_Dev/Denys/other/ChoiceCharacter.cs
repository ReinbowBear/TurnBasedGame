using UnityEngine;

public class ChoiceCharacter : MonoBehaviour
{
    [SerializeField] private int characterID;
    [SerializeField] private int startWeaponID;

    public void SaveCharacter()
    {
        //PlayerPrefs.SetInt("characterID", characterID); //в кавычках имя ключа
        //PlayerPrefs.SetInt("startWeaponID", startWeaponID);
        PlayerPrefs.DeleteAll();
    }
}
