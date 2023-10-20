using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private LifeManager lifeManager;

    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Deduct a life when colliding with an obstacle.
            lifeManager.DeductLife();

            // Optionally, play a sound or trigger other effects.
        }
    }
}

