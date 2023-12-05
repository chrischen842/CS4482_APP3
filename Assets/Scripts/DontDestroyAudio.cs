using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    // Static reference to the instance of our AudioManager
    private static DontDestroyAudio instance = null;

    // On awake, check for any other instances of the audio manager, if there are we destroy it, if not we dont destroy on load
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
