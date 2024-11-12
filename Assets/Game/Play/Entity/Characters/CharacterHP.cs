using System;
using UnityEngine;

public class CharacterHP : Health
{
    public static Action<GameObject> onDead;

    protected override void Death()
    {
        onDead.Invoke(gameObject); //при смерти выпадают предметы если такие есть, это Instantiate, а потому нельзя пихать событие в OnDestroy
        GetCharacter.characterList.Remove(gameObject); //раньше удаление из списка было подписано на событие onDead, но он удалялся раньше чем произойдут нужные операции
        base.Death();
    }
}
