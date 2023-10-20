using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    float currentTime;
    public TMP_Text currentTimeText;

    public static Stopwatch instance; // Add this line at the top of the class with your other public variables.


    // Flag to indicate whether the timer is running
    private bool isTimerRunning = true;

    //Method to reset the timer and start it
    public void ResetAndStartTimer()
    {
        currentTime = 0f;
        isTimerRunning = true;
    }

    void Start()
    {
        // Assign the instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // To ensure it doesn't get destroyed when new scene loads
        }
        else if (instance != this)
        {
            Destroy(gameObject); // If another stopwatch instance exists, destroy this one
            return; // Exit the Start method since the instance is destroyed
        }

        ResetAndStartTimer(); // Reset and start the timer when the scene starts
    }




    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            UpdateStopwatch();
        }
    }

    void UpdateStopwatch()
    {
        currentTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Format the time as MM:SS
        string stopwatchText = string.Format("{0:00}:{1:00}", minutes, seconds);

        currentTimeText.text = stopwatchText;
    }


    // Call this method to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
        PlayerPrefs.SetFloat("LastRecordedTime", currentTime);
        PlayerPrefs.Save();
        currentTime = 0f;  // Reset the timer for the next game session
    }




}