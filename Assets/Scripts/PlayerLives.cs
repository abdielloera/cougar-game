using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;


public class PlayerLives : MonoBehaviour
{
    public List<Image> playerHeads;
    public string gameOverSceneName = "GameOverScene";
    public AudioClip lifeLostSound; // Assign the sound effect to this variable in the Inspector
    public float pitchDecreaseRate = 0.1f; // Rate at which the pitch decreases
    private string dbPath;

    private int lives;
    private HashSet<GameObject> hitObstacles = new HashSet<GameObject>();
    private AudioSource audioSource;

    private void Start()
    {
        lives = playerHeads.Count;
        UpdateLivesDisplay();

        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        dbPath = System.IO.Path.Combine(Application.streamingAssetsPath, "highscores.db");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !hitObstacles.Contains(collision.gameObject))
        {
            hitObstacles.Add(collision.gameObject);
            ReduceLife();
        }
    }

    private void ReduceLife()
    {
        lives--;
        UpdateLivesDisplay();

        // Play the sound effect with decreasing pitch
        if (lifeLostSound != null && audioSource != null)
        {
            audioSource.clip = lifeLostSound;
            audioSource.pitch -= pitchDecreaseRate;
            if (audioSource.pitch < 0f)
            {
                audioSource.pitch = 0f;
            }
            audioSource.Play();
        }

        if (lives <= 0)
        {
            TransitionToGameOver();
        }
    }

    private void UpdateLivesDisplay()
    {
        for (int i = 0; i < playerHeads.Count; i++)
        {
            if (i < lives)
            {
                playerHeads[i].enabled = true;
            }
            else
            {
                playerHeads[i].enabled = false;
            }
        }
    }

 


    private void TransitionToGameOver()
    {
        // Save the antidote count

        PlayerManager.numberOfAntidotes();
        int antidoteCount = PlayerManager.SaveAntidoteCount(); 
        SaveScore(antidoteCount);

        if (Stopwatch.instance != null)
        {
            Stopwatch.instance.StopTimer();
        }

        SceneManager.LoadScene(gameOverSceneName);
    }

   
    public void SaveScore(int antidoteCount)
    {
        string playerName = NameEntryHandler.playerName;
        string connectionString = "URI=file:" + System.IO.Path.Combine(Application.streamingAssetsPath, "highscores.db");

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "INSERT INTO HighScores (PlayerName, AntidoteCount, ScoreDate) VALUES (@PlayerName, @AntidoteCount, @ScoreDate)";
                dbCmd.CommandText = sqlQuery;

                IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                playerNameParam.ParameterName = "@PlayerName";
                playerNameParam.Value = playerName;
                dbCmd.Parameters.Add(playerNameParam);

                IDbDataParameter antidoteCountParam = dbCmd.CreateParameter();
                antidoteCountParam.ParameterName = "@AntidoteCount";
                antidoteCountParam.Value = antidoteCount;
                dbCmd.Parameters.Add(antidoteCountParam);

                IDbDataParameter scoreDateParam = dbCmd.CreateParameter();
                scoreDateParam.ParameterName = "@ScoreDate";
                scoreDateParam.Value = DateTime.Now;
                dbCmd.Parameters.Add(scoreDateParam);

                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }

}
