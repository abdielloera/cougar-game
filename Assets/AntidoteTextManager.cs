using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntidoteTextManager : MonoBehaviour
{
    public static AntidoteTextManager instance; // Singleton instance

    public TextMeshProUGUI antidoteText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAntidoteCount(int count)
    {
        antidoteText.text = "Antidotes: " + count.ToString();
    }
}
