using System;
using UnityEngine;

public class Skyrim : MonoBehaviour
{
    private int index;
    private string element, element1;
    private byte haveSkills ,skillsCount = 4;
    private string[] Race = {"Бретонец", "Имперец", "Норд", "Редгард", "Альтмер", "Босмер", "Данмер", "Орк", "Аргонианин", "Каджит"};
    private string[] Defens = {"тяжёлая_броня", "лёгкая_броня", "магическая_броня", "щитовик", "скрытность",};
    private string[] Weapons = {"лук", "двуручник", "димахер", "одноручное", "разрушение", "колдовство"};
    private string[] Skills = {"карманщик", "илюзия", "красноречие", "взлом", "востановление"};
    private string[] Harvest = {"зельеварение", "кузнечка", "зачарование"};

    void Update()
    {
        if(Input.GetKeyDown("return"))
        {
            GenerateCharacter();
        }
    }

    private void GenerateCharacter()
    {
        haveSkills = 0;
        Debug.Log("==============================");

        index = UnityEngine.Random.Range(0, 100);
        if(index <= 20)
        {
            Debug.Log("вампир");
        }

        index = UnityEngine.Random.Range(0, Race.Length);
        element = Race[index];
        Debug.Log(element);

        index = UnityEngine.Random.Range(0, Defens.Length);
        element = Defens[index];
        Debug.Log(element);
        haveSkills ++;


        if(element != "магическая_броня" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "магическая_броня";
                Debug.Log(element1);
                haveSkills ++;
            }
        }
        if(element != "скрытность" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "скрытность";
                Debug.Log(element1);
                haveSkills ++;
            }
        }
        if(element != "щитовик" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "щитовик";
                Debug.Log(element1);
                haveSkills ++;
            }
        }

        index = UnityEngine.Random.Range(0, Weapons.Length);
        element = Weapons[index];
        Debug.Log(element);
        haveSkills ++;


        if(element != "лук" && element != "щитовик" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "лук";
                Debug.Log(element1);
                haveSkills ++;
            }
        }
        if(element != "колдовство" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "колдовство";
                Debug.Log(element1);
                haveSkills ++;
            }
        }
        if(element != "разрушение" && haveSkills < skillsCount)
        {
            index = UnityEngine.Random.Range(0, 100);
            if(index <= 20)
            {
                element1 = "разрушение";
                Debug.Log(element1);
                haveSkills ++;
            }
        }


        index = UnityEngine.Random.Range(0, Skills.Length);
        element = Skills[index];
        Debug.Log(element);
        haveSkills ++;

        for(byte i = 0; i < Skills.Length; i++)
        {
            if(element != Skills[i] && haveSkills < skillsCount)
            {
                index = UnityEngine.Random.Range(0, 100);
                if(index <= 20)
                {
                    element = Skills[i];
                    Debug.Log(element);
                    haveSkills ++;
                }
            }
        }


        index = UnityEngine.Random.Range(0, Harvest.Length);
        element = Harvest[index];
        Debug.Log(element);
        haveSkills ++;

        for(byte i = 0; i < Harvest.Length; i++)
        {
            if(element != Harvest[i] && haveSkills < skillsCount)
            {
                index = UnityEngine.Random.Range(0, 100);
                if(index <= 20)
                {
                    element = Harvest[i];
                    Debug.Log(element);
                    haveSkills ++;
                }
            }
        }
    }
}
