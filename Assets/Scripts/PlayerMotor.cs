using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public GameObject CharacterModel;
    private Transform initialModelTransform;

    public string moveUp;
    public string moveRight;
    public string moveLeft;

    private bool lookingRight;
    private float turnSpeed = 10f;
	
	//private float midAirAccelleration = 0.4f;
    private float groundSpeed = 8f;
    private float airSpeed = 8f;

    private float jumpSpeed = 14f;
    public bool hasJumped = false;
    public bool isGrounded = false;
    private float fallMultiplier = 6f;
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

        initialModelTransform = CharacterModel.transform;
        
        lookingRight = (transform.position.x < 0);
    }



	void Update() {

        ResetPlayerOnDeath();


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
        bool upInput = Input.GetKeyDown(up);
        bool leftInput = Input.GetKey(left);
        bool rightInput = Input.GetKey(right);


        int yMovementInput = (upInput) ? 1 : 0;
        int xMovementInput = ((leftInput) ? -1:0) + ((rightInput)? 1:0);

        //lookingRight = (xMovementInput != 0f);



        if(xMovementInput != 0){
            lookingRight = xMovementInput > 0;
        }
        // player looks in walking direction
        Quaternion playerOrientation;
        playerOrientation = Quaternion.Euler(new Vector3(0f, initialModelTransform.rotation.y + ((lookingRight) ? 91f : -91f), 0f));
        CharacterModel.transform.rotation = Quaternion.Lerp(CharacterModel.transform.rotation, playerOrientation, Time.deltaTime * turnSpeed);




        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);


        if (isGrounded && !hasJumped)
        {
            rb.velocity = new Vector3(xMovementInput*groundSpeed, rb.velocity.y + yMovementInput*jumpSpeed, 0f);
            if (yMovementInput > 0) hasJumped = true;

        } else {

            //mid air
            rb.velocity = new Vector3(xMovementInput * airSpeed, rb.velocity.y, 0f);

        }


        if (rb.velocity.y <= 0)
            hasJumped = false;

    }

    void ResetPlayerOnDeath()
    {
        if (transform.position.y < -20f)
        {
            // TODO: set spawn positions 
            transform.position = new Vector3(0f, 0f, 0f);

        }
    }




}
