using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public Text livesText; // Reference to a UI text element for displaying lives.

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void DeductLife()
    {
        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            // Implement game over logic here.
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