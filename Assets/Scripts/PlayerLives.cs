using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public List<Image> playerHeads; // Drag the three UI Image components representing the player heads
    public string gameOverSceneName = "GameOverScene"; // Name of the game over scene

    private int lives;
    private HashSet<GameObject> hitObstacles = new HashSet<GameObject>(); // To keep track of hit obstacles

    private void Start()
    {
        lives = playerHeads.Count; // Start with the number of images you added
        UpdateLivesDisplay();
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

        if (lives <= 0)
        {
            TransitionToGameOver();
        }
    }

    private void UpdateLivesDisplay()
    {
        // Hide one player head image for each lost life
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
        // Stop the timer and record the final time
        if (Stopwatch.instance != null) // Ensure there's an instance available
        {
            Stopwatch.instance.StopTimer();
        }

        SceneManager.LoadScene(gameOverSceneName);
    }

}
