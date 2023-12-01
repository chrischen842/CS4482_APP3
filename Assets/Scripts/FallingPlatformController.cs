using System.Collections;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;
    private Vector2 spawnPos;
    // Start is called before the first frame update

    private void Awake()
    {
        spawnPos = new Vector2(transform.position.x, transform.position.y);
    }

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAfterDelay(1f, 5f)); 
        }
    }

    private IEnumerator FallAfterDelay(float fallDelay, float resetDelay)
    {
        yield return new WaitForSeconds(fallDelay);

        _Rigidbody2D.isKinematic = false;
        _Rigidbody2D.gravityScale = 1;

        yield return new WaitForSeconds(resetDelay);

        // Reset the platform
        _Rigidbody2D.position = spawnPos;
        _Rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        _Rigidbody2D.velocity = Vector2.zero; // Reset velocity
        _Rigidbody2D.isKinematic = true; // Make it kinematic again
    }
}
