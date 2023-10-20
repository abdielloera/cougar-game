using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int highScore;
    public Text timerText; // Drag and drop the UI Text element here in the Inspector
    public Text highScoreText;

    void Start()
    {
        // Reset the score when the game starts
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        score++;
        timerText.text = "Score: " + score;

        if(highScore < score)
        {
            IncreaseScore();
        }
    }

    public void IncreaseScore()
    {

        // Update and save the high score if a new high score is achieved
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

    }

    void UpdateHighScoreText()
    {
        // Update the UI Text element to display the high score
        highScoreText.text = "High Score: " + highScore;
    }

    void GameOver()
    {
        // Stop the timer when the game ends
        // You can also save the final time as the player's score
    }


}
