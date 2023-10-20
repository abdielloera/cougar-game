using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the last recorded time from PlayerPrefs
        float lastRecordedTime = PlayerPrefs.GetFloat("LastRecordedTime", 0f);

        int minutes = Mathf.FloorToInt(lastRecordedTime / 60);
        int seconds = Mathf.FloorToInt(lastRecordedTime % 60);

        // Format the time as MM:SS
        string lastRecordedTimeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        pointsText.text = "TIME: " + lastRecordedTimeText;


    }

    public void RestartButton()
    {
        // Reset saved time
        PlayerPrefs.SetFloat("LastRecordedTime", 0f);
        PlayerPrefs.Save();

        // Destroy the stopwatch instance before reloading the main scene
        if (Stopwatch.instance != null)
        {
            Destroy(Stopwatch.instance.gameObject);
        }

        SceneManager.LoadScene("MainScene");
    }

    public void ExitButton()
    {
        // Reset saved time
        PlayerPrefs.SetFloat("LastRecordedTime", 0f);
        PlayerPrefs.Save();

        // Destroy the stopwatch instance before reloading the main scene
        if (Stopwatch.instance != null)
        {
            Destroy(Stopwatch.instance.gameObject);
        }

        SceneManager.LoadScene("Menu");
    }

}

