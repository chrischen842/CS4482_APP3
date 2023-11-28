using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAfterDelay(1f)); 
        }
    }

    private IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _Rigidbody2D.isKinematic = false;
        _Rigidbody2D.gravityScale = 1;
        
    }
}
