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

            int displayCount = Mathf.Min(highscores.highscoreEntryList.Count, 10);
            for (int i = 0; i < displayCount; i++)
            {
                HighScoreEntry highscoreEntry = highscores.highscoreEntryList[i];

                Transform entryTransform = Instantiate(HighScoreEntryTemplate, HighScoreEntryContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -20f * i);
                entryTransform.gameObject.SetActive(true);

                Text rankEntry = entryTransform.Find("rankEntry").GetComponent<Text>();
                Text PlayerName = entryTransform.Find("PlayerName").GetComponent<Text>();
                Text scoreEntry = entryTransform.Find("scoreEntry").GetComponent<Text>();
                Text antidotesEntry = entryTransform.Find("antidotesEntry").GetComponent<Text>();
                Text timeEntry = entryTransform.Find("timeEntry").GetComponent<Text>();

                if (rankEntry != null && PlayerName != null && scoreEntry != null && antidotesEntry != null && timeEntry != null)
                {
                    rankEntry.text = (i + 1).ToString();
                    PlayerName.text = highscoreEntry.PlayerName;
                    scoreEntry.text = highscoreEntry.score.ToString();
                    antidotesEntry.text = highscoreEntry.antidotesCollected.ToString();
                    timeEntry.text = FormatTime(highscoreEntry.LastRecordedTime);
                }
                else
                {
                    Debug.LogError("One or more text components are missing in the HighScoreEntryTemplate.");
                    if (rankEntry == null) Debug.LogError("rankEntry component is missing.");
                    if (PlayerName == null) Debug.LogError("PlayerName component is missing.");
                    if (scoreEntry == null) Debug.LogError("PlayerName component is missing.");
                    if (antidotesEntry== null) Debug.LogError("PlayerName component is missing.");
                    if (timeEntry== null) Debug.LogError("PlayerName component is missing.");




                    // Repeat for scoreEntry, antidotesEntry, and timeEntry

                }
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