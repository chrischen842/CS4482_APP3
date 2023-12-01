using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject button;

    private void Awake()
    {
        button.SetActive(false);
    }

    private void Update()
    {
        if (nameInputField.text.Length > 0 && nameInputField.text.Length < 6)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text; // Assume you have a reference to your InputField
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

}
