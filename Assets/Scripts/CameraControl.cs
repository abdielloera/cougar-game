using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player
    public float smoothing = 5f; // Smoothing factor for camera movement

    private Vector3 offset; // Distance between player and camera

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the offset to the default position between the camera and player
        offset = transform.position - target.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the new position for the camera to follow the player
            Vector3 targetPosition = target.position + offset;
            // Interpolate smoothly between the current position and the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
