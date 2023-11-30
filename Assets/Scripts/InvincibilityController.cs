using UnityEngine;
using System.Collections;

public class InvincibilityController : MonoBehaviour
{
    private bool invincible = false;
    private GameObject shieldInstance;
    private float invincibilityDuration = 8f; // Set the initial duration of invincibility in seconds
    private bool resetDurationOnNextPowerUp = false;

    public bool IsInvincible()
    {
        return invincible;
    }

    public void ActivateInvincibility(GameObject newShieldInstance)
    {
        if (!invincible)
        {
            invincible = true;
            shieldInstance = newShieldInstance;

            // Additional power-up effects or logic

            if (resetDurationOnNextPowerUp)
            {
                // If flag is set, reset the invincibility duration
                ResetInvincibilityDuration();
                resetDurationOnNextPowerUp = false;
            }

            // Start a coroutine to deactivate invincibility after the specified duration
            StartCoroutine(DeactivateInvincibilityAfterDelay());
        }
        else
        {
            // If invincibility is already active, set the flag to reset duration on the next power-up
            resetDurationOnNextPowerUp = true;
        }
    }

    private IEnumerator DeactivateInvincibilityAfterDelay()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        DeactivateInvincibility();
    }

    private void DeactivateInvincibility()
    {
        if (invincible)
        {
            invincible = false;

            if (shieldInstance != null)
            {
                Destroy(shieldInstance);
            }
        }
    }

    private void ResetInvincibilityDuration()
    {
        // Reset the invincibility duration as needed
        Destroy(shieldInstance);
        invincibilityDuration = 8f;
    }
}
