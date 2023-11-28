using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfAntidotes;
    public Text antidotesTextUI;
    // Start is called before the first frame update
    void Start()
    {
        numberOfAntidotes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        antidotesTextUI.text = "Antidotes: " + numberOfAntidotes;
    }

    public static void SaveAntidoteCount()
    {
        PlayerPrefs.SetInt("LastAntidoteCount", numberOfAntidotes);
        PlayerPrefs.Save();
    }

}
