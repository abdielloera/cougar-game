using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Include this to work with UI elements
using TMPro; // Add this for TextMeshPro support


public class NameEntryHandler : MonoBehaviour
{
    // Declare variables to reference the input field and store the player's name
    public TMP_InputField playerNameInput; // Use TMP_InputField instead of InputField
    public static string playerName;
    public static int playerScore;


    // Function to be called when the player submits their name
    public void CapturePlayerName()
    {
        playerName = playerNameInput.text;

        // Here you can add additional functionality like checking if the name is valid
        if (!string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player Name: " + playerName);
            // You can add more code here to handle the captured name
        }
        else
        {
            Debug.Log("Player name is empty");
        }
    }

    // Example function to be called elsewhere (e.g., when starting the game)
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            // Start the game with the captured player name
            Debug.Log("Game starting with player: " + playerName);
        }
        else
        {
            Debug.Log("Please enter a player name first.");
        }
    }

    // Calculate the player's score based on antidotes and time
    private int CalculatePlayerScore()
    {
        int numberOfAntidotes = PlayerManager.numberOfAntidotes; // Get the number of antidotes collected
        float timeLasted = PlayerPrefs.GetFloat("LastRecordedTime", 0f); // Get the time lasted from PlayerPrefs

        int score = numberOfAntidotes + (int)timeLasted; // Example score calculation
        return score;
    }

}
