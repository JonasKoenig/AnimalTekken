using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public string moveUp;
    public string moveRight;
    public string moveLeft;
	
	private float speed = 0.2f;
    private float jumpSpeed = 1.2f;

    private KeyCode up;
	private KeyCode left;
	private KeyCode right;
    private Rigidbody rb;

	void Start () {
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveUp, true);
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveLeft, true);
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), moveRight, true);
        rb = GetComponent<Rigidbody>();
    }

	void Update() {
        if (Input.GetKey(up))
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
