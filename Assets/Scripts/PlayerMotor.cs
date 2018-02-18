using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public string moveUp;
    public string moveRight;
    public string moveLeft;
	
	private float midAirSpeed = 0.5f;
    private float groundSpeed = 10f;
    private float jumpVelocity = 12f;
    private float fallMultiplier = 2f;
    private float lowJumpMultiplier = 2.5f;

    private KeyCode up;
	private KeyCode left;
	private KeyCode right;
    private Rigidbody rb;


    void Awake () {
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveUp, true);
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveLeft, true);
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveRight, true);
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate() {

        // movement according to user input
        InputToMovement();

        // snappy jumping/falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(up))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void InputToMovement()
    {
        bool upInput = Input.GetKey(up);
        bool leftInput = Input.GetKey(left);
        bool rightInput = Input.GetKey(right);

        Vector3 upMovement = Vector3.zero;
        Vector3 leftMovement = Vector3.zero;
        Vector3 rightMovement = Vector3.zero;

        Boolean grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (grounded)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        if (upInput && grounded && rb.velocity.y <= 0)
        {
            upMovement = Vector3.up * jumpVelocity;
        }
        if (leftInput && !rightInput)
        {
            if (grounded)
            {
                rb.velocity = new Vector3(groundSpeed, rb.velocity.y, 0f);
            } else
            {
                leftMovement = Vector3.right * midAirSpeed;
            }
        }
        if (rightInput && !leftInput)
        {
            if (grounded)
            {
                rb.velocity = new Vector3(-groundSpeed, rb.velocity.y, 0f);
            } else
            {
            leftMovement = Vector3.left * midAirSpeed;
            }
        }

        rb.AddForce(upMovement + leftMovement + rightMovement, ForceMode.Impulse);
    }

}
