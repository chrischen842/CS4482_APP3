using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterManager : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public GameObject[] playerPrefabs;
    public Vector2 lastCheckPointPos = new Vector2(-16, -3);
    int characterIndex;

    private void Awake()
    {
        playerName.text = "Player: " + PlayerPrefs.GetString("PlayerName");
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
    }


}
