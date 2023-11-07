using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AntidoteCounter : MonoBehaviour
{
    public Text antidoteCountText; // Make it public

    private int numberOfAntidotes = 0;
    private bool isCounting = true; // Set to false to stop counting

    void Start()
    {
        // Check if the Text component is properly assigned
        if (antidoteCountText == null)
        {
            Debug.LogError("AntidoteCounter script requires a Text component. Drag the Text component into the script's 'Antidote Count Text' field in the Inspector.");
        }

        UpdateAntidoteCount(); // Display initial count

        // Start a coroutine to count antidotes over time (if needed)
        StartCoroutine(CountAntidotes());
    }

    void UpdateAntidoteCount()
    {
        if (antidoteCountText != null)
        {
            antidoteCountText.text = "Antidotes: " + numberOfAntidotes;
        }
    }

    // Coroutine to simulate counting over time
    IEnumerator CountAntidotes()
    {
        while (isCounting)
        {
            // Simulate counting over time
            numberOfAntidotes++;
            UpdateAntidoteCount();

            // Adjust the delay to control the speed of counting
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Call this method when you want to stop counting (e.g., when the game ends)
    public void StopCounting()
    {
        isCounting = false;
    }
}
