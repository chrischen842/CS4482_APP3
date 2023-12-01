using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public GameObject[] skins;
    public GameObject[] names;
    
    public int selectedCharacter;

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        foreach(GameObject player in skins)
        {
            player.SetActive(false);

        }

        foreach(GameObject name in names)
        {
            name.SetActive(false);
        }

        skins[selectedCharacter].SetActive(true);
        names[selectedCharacter].SetActive(true);
    }

    public void NextSkin()
    {
        skins[selectedCharacter].SetActive(false);
        names[selectedCharacter].SetActive(false);
        selectedCharacter++;

        if(selectedCharacter == skins.Length)
        {
            selectedCharacter = 0;
        }

        skins[selectedCharacter].SetActive(true);
        names[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void LastSkin()
    {
        skins[selectedCharacter].SetActive(false);
        names[selectedCharacter].SetActive(false);
        selectedCharacter--;

        if (selectedCharacter == -1)
        {
            selectedCharacter = skins.Length - 1;
        }

        skins[selectedCharacter].SetActive(true);
        names[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}