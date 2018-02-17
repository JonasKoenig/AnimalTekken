using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public string moveUp;
    public string moveRight;
    public string moveLeft;
	
	private float speed = 0.2f;
    private float jumpSpeed = 3f;

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

	void Update() {
        Boolean grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (Input.GetKey(up) && grounded)
        {
            Vector3 direction = new Vector3(0f, jumpSpeed, 0f);
            rb.AddForce(direction, ForceMode.Impulse);
        } else if (Input.GetKey(left))
        {
            Vector3 direction = new Vector3(speed, 0f, 0f);
            rb.AddForce(direction, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(right)) {
            Vector3 direction = new Vector3(-speed, 0f, 0f);
            rb.AddForce(direction, ForceMode.VelocityChange);
        }
    }
}
