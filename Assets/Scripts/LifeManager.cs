using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public Text livesText; // Reference to a UI text element for displaying lives.

    public InvincibilityController invincibilityController; // Reference to your InvincibilityController script.

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void DeductLife()
    {
        Debug.LogError(invincibilityController.IsInvincible());
        // Check if the player is not invincible before deducting a life
        if (invincibilityController == null || !invincibilityController.IsInvincible())
        {
            currentLives--;
            UpdateLivesUI();

            if (currentLives <= 0)
            {
                // Implement game over logic here.
            }
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}
