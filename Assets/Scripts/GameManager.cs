using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedSkin;
    public GameObject Player;
    public GameObject MainPlayer;

    private Sprite playerSprite;

    void Start()
    {
        playerSprite = selectedSkin.GetComponent<SpriteRenderer>().sprite;

        Player.GetComponent<SpriteRenderer>().sprite = playerSprite;
        MainPlayer.GetComponent<SpriteRenderer>().sprite = playerSprite;
    }
}
