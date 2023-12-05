using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public TextMeshProUGUI topScoresText;
    private const int maxTopScores = 5;

    private void Start()
    {
        LoadAndDisplayTopScores();
    }

    private void LoadAndDisplayTopScores()
    {
        topScoresText.text = "Top Scores:\n";
        for (int i = 1; i <= maxTopScores; i++)
        {
            string combinedKey = "TopScore" + i;
            string entryText = $"{i}. N/A\n"; // Default text if there's no score

            if (PlayerPrefs.HasKey(combinedKey))
            {
                string combinedValue = PlayerPrefs.GetString(combinedKey);
                string[] parts = combinedValue.Split(':');
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    string score = parts[1];
                    entryText = $"{i}. {username}: {score}\n"; // Updated text with username and score
                }
            }

            topScoresText.text += entryText;
        }
    }

    public void ClearLeaderboard()
    {
        // Clear the leaderboard data from PlayerPrefs
        for (int i = 1; i <= maxTopScores; i++)
        {
            string combinedKey = "TopScore" + i;
            PlayerPrefs.DeleteKey(combinedKey);
        }
        PlayerPrefs.Save();

        // Update the displayed scores to show "N/A" for each place
        topScoresText.text = "Top Scores:\n";
        for (int i = 1; i <= maxTopScores; i++)
        {
            topScoresText.text += $"{i}. N/A\n";
        }
    }

}
