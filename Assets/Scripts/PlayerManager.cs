using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float fadeDuration = 0.2f; // Duration of the fade-in effect
    public float displayTime = 2f; // How long to display 'You Win' before loading the new scene

    public int score = 0;
    public TextMeshProUGUI scoreDisplay;
    public SceneController sceneController;
    public AudioSource deathSound;
    public AudioSource collectSound;

    public float[] spawnLoc = new float[2];

    private Animator _Animator;
    private SpriteRenderer _SpriteRenderer;
    private bool isInvincible = false;
    private Image youWinScreen;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();


        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
        collectSound = GameObject.Find("CollectSound").GetComponent<AudioSource>();
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
            collectSound.Play();
            increaseScore();
            setScore();
        }

        if (collision.gameObject.CompareTag("Hazard") && !isInvincible)
        {
            Debug.Log("Hit");
            deathSound.Play();
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

        if (collision.gameObject.CompareTag("Level3"))
        {
            Debug.Log("Loaded");
            UpdateScore(PlayerPrefs.GetString("PlayerName"), score);
            sceneController.LoadScene("Level4");
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Loaded");
            youWinScreen = GameObject.Find("YouWin").GetComponent<Image>(); ;
            StartCoroutine(EndLevelSequence());
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

    private IEnumerator EndLevelSequence()
    {
        Debug.Log("You Win!");
        UpdateScore(PlayerPrefs.GetString("PlayerName"), score); // Assuming score is a variable you have access to

        // Start fading in the 'You Win' screen
        float elapsedTime = 0;
        Color color = youWinScreen.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            youWinScreen.color = color;
            yield return null;
        }

        // Wait for displayTime seconds on the 'You Win' screen
        yield return new WaitForSeconds(displayTime);

        // Load the new scene
        sceneController.LoadScene("GameEnd");
    }
}
