using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameEndController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private const int maxTopScores = 5; // Max number of top scores to keep
    public List<(float score, string username)> topScores = new List<(float score, string username)>();

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        int playerScore = PlayerPrefs.GetInt(playerName);
        scoreText.text = playerName + ": " + playerScore;

        LoadTopScores();
        SaveScores(playerScore, playerName);

        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.DeleteKey(playerName + "Score");
        PlayerPrefs.Save();
    }

    private void SaveScores(float currentScore, string username)
    {
        // Load existing top scores first
        LoadTopScores();

        // Add new score along with the username
        topScores.Add((currentScore, username));

        // Sort the list by score in descending order since higher scores are better
        topScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Keep only top 5 scores
        if (topScores.Count > maxTopScores)
        {
            topScores.RemoveRange(maxTopScores, topScores.Count - maxTopScores);
        }

        // Save scores and usernames to PlayerPrefs
        for (int i = 0; i < topScores.Count; i++)
        {
            string combinedKey = "TopScore" + (i + 1);
            string combinedValue = topScores[i].username + ":" + topScores[i].score.ToString("F2"); // F2 to format the float to two decimal places

            PlayerPrefs.SetString(combinedKey, combinedValue);
        }

        PlayerPrefs.Save();
    }

    private void LoadTopScores()
    {
        // Clear the list before loading new scores
        topScores.Clear();

        for (int i = 1; i <= maxTopScores; i++)
        {
            string combinedKey = "TopScore" + i;

            // Check if the combined score and username exist in PlayerPrefs
            if (PlayerPrefs.HasKey(combinedKey))
            {
                // Retrieve the combined score and username
                string combinedValue = PlayerPrefs.GetString(combinedKey);
                string[] parts = combinedValue.Split(':');
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    if (float.TryParse(parts[1], out float score))
                    {
                        // Add them as a tuple to the topScores list
                        topScores.Add((score, username));
                    }
                }
            }
        }

        // After loading, sort the list in case it's not in order
        topScores.Sort((a, b) => b.score.CompareTo(a.score));
    }
}
