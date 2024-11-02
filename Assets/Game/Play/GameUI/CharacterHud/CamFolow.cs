using System.Collections;
using UnityEngine;

public class CamFolow : MonoBehaviour
{
    private GameObject CamObject;
    private int characterID;
    [SerializeField] private float duration;

    private Coroutine myCoroutine;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeElapsed;

    void Start()
    {
        CamObject = Camera.main.transform.root.gameObject;

        GameObject character = GetComponent<CharacterHud>().myCharacter; //с такой записью может багатся если персонаж умирает или заменяется
        characterID = GetCharacter.CharacterList.IndexOf(character);
    }


    public void CamToCharacter()
    {
        GetNewCharacter(characterID);
        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(CameraFolow(GetCharacter.CharacterList[characterID].transform.position));
        }
        else
        {
            startPos = CamObject.transform.position;
            endPos = GetCharacter.CharacterList[characterID].transform.position;
            timeElapsed = 0f;
        }
    }

    private IEnumerator CameraFolow(Vector3 targetCharacter) //по идеи должно было бы конфликтовать с HoldCam() в CamObject, но почему то просто перекрывает
    {
        startPos = CamObject.transform.position;
        endPos = targetCharacter;
   
        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            CamObject.transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        CamObject.transform.position = endPos;
        myCoroutine = null;
    }

    private void GetNewCharacter(int characterID)
    {
        if (ClickCharacter.choseCharacter != null)
        {
            ClickCharacter.choseCharacter.FalseCharacter();
        }

        LogicCharacter newCharacter = GetCharacter.CharacterList[characterID].GetComponent<LogicCharacter>();
        
        ClickCharacter.choseCharacter = newCharacter;
        newCharacter.ChoseCharacter();
        ClickCharacter.onChoiceCharacter.Invoke();
    }
}
