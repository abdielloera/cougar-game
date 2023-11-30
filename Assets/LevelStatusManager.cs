using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelStatusManager : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Assign this in the inspector
    private float timer = 0f;
    private int currentLevel = 0;

    private string[] levels = new string[] 
    {
        "Beginner", "Novice", "Intermediate", "Advanced", "Expert", "Master"
    };

    void Start()
    {
        // Initialize the status text with the first level ("Beginner") at the start of the game
        statusText.text = "Level: " + levels[currentLevel];
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check for level up every 2 minutes (120 seconds)
        if (timer >= (currentLevel + 1) * 120f)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        if (currentLevel < levels.Length)
        {
            statusText.text = "Level: " + levels[currentLevel];
        }
        else
        {
            // Optional: Do something when the maximum level is reached
            statusText.text = "Level: Master!";
        }
    }
}
