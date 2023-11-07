using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes 
    public float jumpForce;
    public float Gravity = -20;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            direction.y = -1;
            //input for player to jump, but only allow to jump if the player is grounded
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
            //SwipeManager.swipeUp for mobile version
            {                                 //Input.GetKeyDown(KeyCode.UpArrow) for laptop
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        //get input for switching lanes
        if (Input.GetKeyDown(KeyCode.RightArrow))//SwipeManager.swipeRight for mobile version
        {                                       //Input.GetKeyDown(KeyCode.RightArrow) for laptop
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))//SwipeManager.swipeLeft for mobile version
        {                                       //Input.GetKeyDown(KeyCode.LeftArrow) for laptop
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
        //transform.position = Vector3.Lerp(transform.position, targetPosition,
        //80 * Time.deltaTime);
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
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
