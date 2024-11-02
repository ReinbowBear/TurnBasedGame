using System;
using System.Collections;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    public static Action<GameObject> onAttack;

    [SerializeField] LayerMask rayLayer;
    [Space]
    public byte mannaCount;

    [HideInInspector] public Weapon myWeapon;
    [HideInInspector] public Ability myAbility;
    [HideInInspector] public Equipment myEquip;

    private Camera cam;
    public Coroutine coroutine;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool wasAttaking;

    void Awake()
    {
        cam = Camera.main;
        myWeapon = GetComponent<Weapon>();
    }

    public void CharacterAttack(int index)
    {
        if (index == 0)
        {
            coroutine = StartCoroutine(PrepareToAttack(myWeapon));
        }
        else if (myAbility != null)
        {
            coroutine = StartCoroutine(PrepareToAttack(myAbility));
        }
    }

    private IEnumerator PrepareToAttack(Applicable myTools) //Applicable это общий абстракктный класс для оружия и способностей, работает как переходник
    {
        isAttacking = true;
        myTools.Prepare();
        
        while (isAttacking == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                if (hit.transform.CompareTag("ActiveTile"))
                {
                    myTools.ChangeDirection(hit.transform.position);
                }
            }

            if (GameKeyboard.gameInput.Player.Mouse_0.triggered)
            {
                Attack(myTools);
            }
            yield return null;
        }
        coroutine = null;
    }

    private void Attack(Applicable myTools)
    {
        isAttacking = false;
        wasAttaking = true;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
        {
            if (myTools is Weapon)
            {
                myWeapon.False();
                TurnManager.characterAttacks.Add(myWeapon);
            }
            else
            {
                myTools.Activate();
            }

            onAttack.Invoke(gameObject);
        }
    }


    public void FalseAttack()
    {
        if (coroutine != null)
        {
            myWeapon.False();

            if (myAbility != null)
            {
                myAbility.False();
            }

            StopCoroutine(coroutine);
            coroutine = null;
            isAttacking = false;
        }
    }


    public void ChangeItem<T>(T newItem, bool saveItem = true)
    {
        if (newItem is Ability) //хотелось бы подставить переменную под замену ведь это 1 и тот же код НО Я НЕ ЕБУ КААААААК!!! (не делать же дженерик массивы или листы)
        {
            if (myAbility != null)
            {
                Destroy(myAbility);
            }

            if (saveItem != false)
            {
                myAbility = gameObject.AddComponent<Ability>();
            }
        }
        else
        {
            if (myEquip != null)
            {
                Destroy(myEquip);
            }
            if (saveItem != false)
            {
                myEquip = gameObject.AddComponent<Equipment>();
            }
        }
    }
}
