using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;//initial speed of the player's movement
    public Rigidbody rb; //physics of the player (movements)
    bool onTheGround = true;

    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes 
    public float jumpForce = 10;
    public float gravity;
    public float horizontalMoveSpeed;

    private void FixedUpdate()
    {
        //make player fall faster
        Physics.gravity = new Vector3(0, gravity, 0);

        //player moving forward at speed every second
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

        //player's position changes
        rb.MovePosition(rb.position + forwardMove);
    }

    // Update is called once per frame
    void Update()
    {
        //input for player to jump, but only allow to jump if the player is grounded
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            && onTheGround) //SwipeManager.swipeUp for mobile version
        {                                 //Input.GetKeyDown(KeyCode.UpArrow) for laptop
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onTheGround = false;
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
            desiredLane --;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //calculate the new position of the player once the player changes lanes

        Vector3 targetPosition = transform.position.z * transform.forward +
            transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //smooth transition left and right
        transform.position = Vector3.Lerp(transform.position, targetPosition,
            80 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Ground"))
        {
            onTheGround = true;
        }
    }

}
