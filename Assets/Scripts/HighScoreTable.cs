using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    public NameEntryHandler nameEntryHandler; // Reference to the NameEntryHandler
    public Stopwatch stopwatch;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        // Sort the highscore list
        if (highscores != null && highscores.highscoreEntryList != null)
        {
            highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

            DisplayHighScores(highscores);
        }
    }

    private void DisplayHighScores(HighScores highscores)
    {
        foreach (Transform child in entryContainer)
        {
            Destroy(child.gameObject);
        }

        int displayCount = Mathf.Min(highscores.highscoreEntryList.Count, 10); // Show top 10 scores
        for (int i = 0; i < Mathf.Min(highscores.highscoreEntryList.Count, 10); i++)
        {
            HighScoreEntry highscoreEntry = highscores.highscoreEntryList[i];

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -20f * i);
            entryTransform.gameObject.SetActive(true);

            // Assign rank based on position in the list (i + 1)
            entryTransform.Find("rankEntry").GetComponent<Text>().text = (i + 1).ToString();

            // Player Name
            entryTransform.Find("PlayerName").GetComponent<Text>().text = highscoreEntry.PlayerName;

            // Score
            entryTransform.Find("scoreEntry").GetComponent<Text>().text = highscoreEntry.score.ToString();

            // Antidotes Collected
            entryTransform.Find("antidotesEntry").GetComponent<Text>().text = highscoreEntry.antidotesCollected.ToString();

            // Time Lasted
            entryTransform.Find("timeEntry").GetComponent<Text>().text = FormatTime(highscoreEntry.LastRecordedTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void AddNewHighScoreEntry(string name, int score, int antidotes, float time)
    {
        HighScoreEntry newEntry = new HighScoreEntry
        {
            PlayerName = name,
            score = score,
            antidotesCollected = antidotes,
            LastRecordedTime = time
        };


        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        if (highscores == null)
        {
            highscores = new HighScores { highscoreEntryList = new List<HighScoreEntry>() };
        }

        highscores.highscoreEntryList.Add(newEntry);

        // Sort and potentially limit the size of the list
        highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));
        if (highscores.highscoreEntryList.Count > 10) // For instance, keep only top 10
        {
            highscores.highscoreEntryList.RemoveRange(10, highscores.highscoreEntryList.Count - 10);
        }

        jsonString = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScoreTable", jsonString);
        PlayerPrefs.Save();

        Debug.Log("Saving High Scores: " + jsonString);

    }

}

[System.Serializable]
public class HighScores
{
    public List<HighScoreEntry> highscoreEntryList;
}

[System.Serializable]
public class HighScoreEntry
{

    public string PlayerName;
    public int score;
    public int antidotesCollected;
    public float LastRecordedTime;
}
