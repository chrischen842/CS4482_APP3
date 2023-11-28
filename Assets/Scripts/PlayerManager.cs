using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreDisplay;
    public SceneController sceneController;
    public float[] spawnLoc = new float[2];

    private Animator _Animator;

    private void Start()
    {
        _Animator = GetComponent<Animator>();

        setScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collision.gameObject.SetActive(false);
            increaseScore();
            setScore();
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hit");
            StartCoroutine(RespawnWithDelay(0f));
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Loaded");
            sceneController.LoadScene("Level2");
        }
    }

    private void setScore()
    {
        scoreDisplay.text = "Score: " + score;
    }

    private void increaseScore()
    {
        score += 1000;
    }

    private void decreaseScore()
    {
        if ((score - 50) > 0)
        {
            score = score - 50;
        }
        else
        {
            score = 0;
        }
    }

    private IEnumerator RespawnWithDelay(float delay)
    {
        Debug.Log("Dead");
        decreaseScore();
        setScore();
        _Animator.Play("PlayerDeath");

        yield return new WaitForSeconds(delay);

        transform.position = new Vector2(spawnLoc[0], spawnLoc[1]);
        _Animator.Play("PlayerSpawn");
    }
}
