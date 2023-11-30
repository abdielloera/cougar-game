using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;



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
        int antidoteCount = PlayerManager.SaveAntidoteCount(); // Access the field/property directly
        float timeLasted = PlayerPrefs.GetFloat("LastRecordedTime", 0f); // Get the time lasted from PlayerPrefs

        // the score is calculated as antidotes + time
        int score = antidoteCount + (int)timeLasted;

        // Get the player's name from NameEntryHandler
        string playerName = NameEntryHandler.playerName;

        // Add score to HighScoreTable
        HighScoreTable.AddNewHighScoreEntry(score, playerName, antidoteCount, timeLasted);

        
        if (Stopwatch.instance != null)
        {
            Stopwatch.instance.StopTimer();
        }

        SceneManager.LoadScene(gameOverSceneName);
    }

   
   

}
