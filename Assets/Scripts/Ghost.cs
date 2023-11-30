using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public GameObject shieldPrefab;
    private GameObject shieldInstance;
    public float selfDestructDuration = 30f;


    private InvincibilityController invincibilityController;

    void Start()
    {
        // Get the InvincibilityController component from the player GameObject
        invincibilityController = FindObjectOfType<InvincibilityController>();
        StartCoroutine(SelfDestructTimer());

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (invincibilityController != null)
            {
                // Instantiate the transparent shield sphere
                shieldInstance = Instantiate(shieldPrefab, other.transform.position, Quaternion.identity);
                // Parent the shield to the player (optional)
                shieldInstance.transform.parent = other.transform;

                // Activate the invincibility shield and pass the reference
                invincibilityController.ActivateInvincibility(shieldInstance);

                // Additional power-up effects or logic
                Destroy(gameObject); // Destroy the power-up object
            }
        }
    }

    void Update()
    {
        // Check if the shield is active and move it with the player (optional)
        if (invincibilityController != null && invincibilityController.IsInvincible() && shieldInstance != null)
        {
            shieldInstance.transform.position = transform.position;
        }
    }

    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructDuration);

        // Destroy the power-up object when the timer expires
        Destroy(gameObject);
    }
}
