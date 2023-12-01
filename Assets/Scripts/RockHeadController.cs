using System.Collections;
using UnityEngine;

public class RockHeadController : MonoBehaviour
{
    private Animator _Animator;
    private Rigidbody2D _Rigidbody2D;
    private Vector2 spawnPos;

    // Start is called before the first frame update

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        spawnPos = new Vector2(transform.position.x, transform.position.y);
    }

    private void Start()
    {
        StartCoroutine(BlinkRoutine()); // Start the blinking routine
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _Rigidbody2D.isKinematic = false;
            _Rigidbody2D.gravityScale = 1;

            StartCoroutine(reset(6f));
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (true) 
        {
            _Animator.SetBool("Blink", true); 
            yield return new WaitForSeconds(0.1f); // Assuming the blink animation is quick, like 0.1 seconds
            _Animator.SetBool("Blink", false); // End the blink animation

            yield return new WaitForSeconds(5f); // Wait 5 seconds before blinking again
        }
    }

    private IEnumerator reset(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the platform
        _Rigidbody2D.position = spawnPos;
        _Rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        _Rigidbody2D.velocity = Vector2.zero; // Reset velocity
        _Rigidbody2D.isKinematic = true; // Make it kinematic again
    }
}
