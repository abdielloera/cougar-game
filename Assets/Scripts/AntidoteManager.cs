using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateAntidoteCount(); // Update the count when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.numberOfAntidotes += 1;
            UpdateAntidoteCount(); // Update the count when an antidote is picked up
            Destroy(gameObject);
        }
    }

    void UpdateAntidoteCount()
    {
        // Use the AntidoteTextManager instance to update the count
        AntidoteTextManager.instance.UpdateAntidoteCount(PlayerManager.numberOfAntidotes);
    }
}

