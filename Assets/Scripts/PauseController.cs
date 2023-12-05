using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;

    [SerializeField]
    private bool isPaused;

    //Makes sure that the game is not paused
    void Start()
    {
        PauseGame(false);
    }

    //Constantly checking if the user has paused the game
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    public void PauseGame(bool paused)
    {
        if (paused)
        {
            //Show the pause menu
            pauseCanvas.SetActive(true);
        }
        else
        {
            //Hide the pause menu
            pauseCanvas.SetActive(false);
        }

        isPaused = paused;
        Time.timeScale = paused ? 0 : 1;
    }
}
