using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;


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

    public static List<(string playerName, int score, string date)> GetTopScores()
    {
        List<(string playerName, int score, string date)> topScores = new List<(string playerName, int score, string date)>();
        string connectionString = "URI=file:" + System.IO.Path.Combine(Application.streamingAssetsPath, "highscores.db");

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT PlayerName, AntidoteCount, ScoreDate FROM HighScores ORDER BY AntidoteCount DESC LIMIT 5";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string playerName = reader.GetString(0);
                        int score = reader.GetInt32(1);
                        string date = reader.GetDateTime(2).ToString("yyyy-MM-dd");
                        topScores.Add((playerName, score, date));
                    }
                    reader.Close();
                }
            }
            dbConnection.Close();
        }

        return topScores;
    }


}
