using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingCharacter : MonoBehaviour
{
    private Renderer characterRenderer;
    private Material originalMaterial;
    public float blinkInterval = 0.2f;
    private bool isBlinking = false;

    private void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        originalMaterial = characterRenderer.material;
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleVisibility", 0f, blinkInterval);
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            CancelInvoke("ToggleVisibility");
            characterRenderer.material = originalMaterial; // Reset the material
        }
    }

    private void ToggleVisibility()
    {
        if (characterRenderer.enabled)
        {
            characterRenderer.enabled = false;
        }
        else
        {
            characterRenderer.enabled = true;
        }
    }
}

