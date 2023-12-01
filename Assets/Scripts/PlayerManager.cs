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
    private SpriteRenderer _SpriteRenderer;
    private bool isInvincible = false;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        scoreDisplay = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        sceneController = GameObject.FindObjectOfType<SceneController>();

        score = GetScore(PlayerPrefs.GetString("PlayerName"));
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

        if (collision.gameObject.CompareTag("Hazard") && !isInvincible)
        {
            Debug.Log("Hit");
            StartCoroutine(RespawnWithDelay(0f));
        }

        //Load next levels
        if (collision.gameObject.CompareTag("Level0"))
        {
            Debug.Log("Loaded");
            UpdateScore(PlayerPrefs.GetString("PlayerName"), score);
            sceneController.LoadScene("Level1");
        }

        if (collision.gameObject.CompareTag("Level1"))
        {
            Debug.Log("Loaded");
            UpdateScore(PlayerPrefs.GetString("PlayerName"), score);
            sceneController.LoadScene("Level2");
        }

        if (collision.gameObject.CompareTag("Level2"))
        {
            Debug.Log("Loaded");
            UpdateScore(PlayerPrefs.GetString("PlayerName"), score);
            sceneController.LoadScene("Level3");
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

    public int GetScore(string characterName)
    {
        return PlayerPrefs.GetInt(characterName, 0); 
    }

    public void UpdateScore(string characterName, int newScore)
    {
        PlayerPrefs.SetInt(characterName, newScore);
        PlayerPrefs.Save(); 
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

        isInvincible = true;
        StartCoroutine(InvincibilityBlink(2f)); // Start the invincibility blink coroutine
        yield return new WaitForSeconds(2f); // Invincibility duration
        isInvincible = false;

        // Make sure the sprite is fully visible after invincibility ends
        _SpriteRenderer.color = new Color(_SpriteRenderer.color.r, _SpriteRenderer.color.g, _SpriteRenderer.color.b, 1f);
    }

    private IEnumerator InvincibilityBlink(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            // Toggle the visibility
            _SpriteRenderer.color = new Color(_SpriteRenderer.color.r, _SpriteRenderer.color.g, _SpriteRenderer.color.b, _SpriteRenderer.color.a == 1f ? 0.5f : 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
