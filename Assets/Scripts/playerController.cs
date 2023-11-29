using UnityEngine;

public class playerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float increasedSpeed = 10f; // Speed after Z position reaches 1000
    private bool hasReachedZ1000 = false;
<<<<<<< Updated upstream
    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 4; // the distance between two lanes 
=======
    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes 
>>>>>>> Stashed changes
    public float jumpForce;
    public float Gravity = -20;

    public AudioClip jumpSound; // Jump sound effect
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && jumpSound != null)
        {
            audioSource.clip = jumpSound; // Assign the jump sound to the audio source
        }
        else
        {
            Debug.LogError("AudioSource or jumpSound is not set in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (transform.position.z >= 100 && !hasReachedZ1000)
=======
        if (transform.position.z >= 200 && !hasReachedZ1000)
>>>>>>> Stashed changes
        {
            forwardSpeed = increasedSpeed;
            hasReachedZ1000 = true;
        }
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            direction.y = -1;
            //input for player to jump, but only allow to jump if the player is grounded
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        //get input for switching lanes
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //calculate the new position of the player once the player changes lanes

        Vector3 targetPosition = transform.position.z * transform.forward +
            transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //smooth transition left and right
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;

        // Play the jump sound effect
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        else
        {
            Debug.LogError("AudioSource or jumpSound is not set!");
        }
    }

}
