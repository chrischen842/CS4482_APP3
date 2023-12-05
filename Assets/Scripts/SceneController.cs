using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string username;

    public void Awake()
    {
        if (GameObject.Find("PauseMenu") != null)
        {
            Debug.Log("Level Scene");
        }

        username = PlayerPrefs.GetString("PlayerName");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Check if the PauseMenu GameObject exists in the scene
        if (GameObject.Find("PauseMenu") != null)
        {
            Debug.Log("Quitting level session: " + PlayerPrefs.GetString("PlayerName"));
            clearUser();
        }
        else
        {
            Debug.Log("Quitting game from a non-level scene.");
        }

        // Quit the application
        Application.Quit();
    }

    public void clearUser()
    {
        PlayerPrefs.DeleteKey(username);
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.Save();
    }
}
