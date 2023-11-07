using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteManager : MonoBehaviour
{
    private static int numberOfAntidotes = 0; // Static to keep count across all instances

    public static event System.Action<int> OnAntidoteCollected;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            numberOfAntidotes++;
            Debug.Log("Antidotes: " + numberOfAntidotes);
            Destroy(gameObject);

            // Notify observers (UI) that an antidote is collected
            OnAntidoteCollected?.Invoke(numberOfAntidotes);
        }
    }
}
