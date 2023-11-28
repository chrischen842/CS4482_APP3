using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int score = 0;

    public Animator _Animator;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collect")
        {
            _Animator.SetBool("Collected", true);
            collision.enabled = false;
            score++;
        }
    }
}
