using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public List<Image> playerHeads;
    public string gameOverSceneName = "GameOverScene";
    public AudioClip lifeLostSound; // Assign the sound effect to this variable in the Inspector
    public float pitchDecreaseRate = 0.1f; // Rate at which the pitch decreases

    private int lives;
    private HashSet<GameObject> hitObstacles = new HashSet<GameObject>();
    private AudioSource audioSource;

    public InvincibilityController invincibilityController; // Reference to your InvincibilityController script.

    private void Start()
    {
        lives = playerHeads.Count;
        UpdateLivesDisplay();

        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !hitObstacles.Contains(collision.gameObject))
        {
            hitObstacles.Add(collision.gameObject);

            // Check if the player is not invincible before reducing life
            if (invincibilityController == null || !invincibilityController.IsInvincible())
            {
                ReduceLife();
            }
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
        if (Stopwatch.instance != null)
        {
            Stopwatch.instance.StopTimer();
        }

        SceneManager.LoadScene(gameOverSceneName);
    }
}
