using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Include this to work with UI elements
using TMPro; // Add this for TextMeshPro support


public class NameEntryHandler2 : MonoBehaviour
{
    // Declare variables to reference the input field and store the player's name
    public TMP_InputField playerNameInput; // Use TMP_InputField instead of InputField
    public static string PlayerName;
    private string secretKey = "YourSecretKey"; // Replace with your actual secret key


    // Function to be called when the player submits their name
    public void CapturePlayerName()
    {
        PlayerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(PlayerName))
        {
            Debug.Log("Player Name: " + PlayerName);
            PlayerPrefs.SetString("PlayerName", PlayerName); // Save player name
            PlayerPrefs.Save(); // Don't forget to save PlayerPrefs
        }
        else
        {
            Debug.Log("Player name is empty");
        }
    }


    // Example function to be called elsewhere (e.g., when starting the game)
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(PlayerName))
        {
            // Start the game with the captured player name
            Debug.Log("Game starting with player: " + PlayerName);
        }
        else
        {
            Debug.Log("Please enter a player name first.");
        }
    }
}
