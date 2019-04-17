using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChampSelect : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");


        characterList = new GameObject[transform.childCount];

        //Fill Array with Models
        for (int i = 0; i < transform .childCount; i++)
        characterList[i] = transform.GetChild(i).gameObject;
        
        //Toggle off Models
        foreach(GameObject go in characterList)
            go.SetActive(false);

        //Toggle on First Index
        if (characterList[index])
            characterList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        //Toggle off current model
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;

        //Toggle on new model
        characterList[index].SetActive(true);
    }


    public void ToggleRight()
    {
        //Toggle off current model
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;

        //Toggle on new model
        characterList[index].SetActive(true);
    }

    public void SelectButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene(2);
    }

}
