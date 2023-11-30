using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;
    public TMP_Text antidoteText;
   

    public Transform HighScoreEntryContainer;
    public Transform HighScoreEntryTemplate;



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
        DisplayAntidoteCount();
        DisplayHighScores();


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

    void DisplayAntidoteCount()
    {
        // Retrieve the last antidote count from PlayerPrefs
        int lastAntidoteCount = PlayerPrefs.GetInt("LastAntidoteCount", 0);

        // Set the antidote text
        antidoteText.text = "ANTIDOTES: " + lastAntidoteCount;
    }

    private void DisplayHighScores()
    {
        if (HighScoreEntryContainer == null || HighScoreEntryTemplate == null)
        {
            Debug.LogError("HighScoreEntryContainer or HighScoreEntryTemplate is not assigned in the Inspector.");
            return;
        }

        HighScoreEntryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highscores != null && highscores.highscoreEntryList != null)
        {
            highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

            foreach (Transform child in HighScoreEntryContainer)
            {
                Destroy(child.gameObject);
            }

            int displayCount = Mathf.Min(highscores.highscoreEntryList.Count, 10); // Show top 10 scores
            for (int i = 0; i < displayCount; i++)
            {
                HighScoreEntry highscoreEntry = highscores.highscoreEntryList[i];

                Transform entryTransform = Instantiate(HighScoreEntryTemplate, HighScoreEntryContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -20f * i);
                entryTransform.gameObject.SetActive(true);

                // Set the UI elements for each high score entry
                entryTransform.Find("rankEntry").GetComponent<Text>().text = (i + 1).ToString();
                entryTransform.Find("PlayerName").GetComponent<Text>().text = highscoreEntry.playerName;
                entryTransform.Find("scoreEntry").GetComponent<Text>().text = highscoreEntry.score.ToString();
                entryTransform.Find("antidotesEntry").GetComponent<Text>().text = highscoreEntry.antidotesCollected.ToString();
                entryTransform.Find("timeEntry").GetComponent<Text>().text = FormatTime(highscoreEntry.timeLasted);
            }
        }


    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);



    }

}