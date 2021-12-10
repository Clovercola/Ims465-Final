using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    private bool inJump = false;
    private float preventJump;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to the middle of the screen.
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFell(); //Kills player if they fell off the map.
        if (inJump == true)
        {
            TestReadyToJump();
        }
        ApplyGravity(); //Simple method to make the jump less floaty by constantly pulling the player down.
        UpdateMovement(); //Handle the movement for the player.
        UpdateCamera(); //Moves camera to appropriate position relative to the player.
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject manager = GameObject.Find("Game Manager");
        if (other.name.Equals("Goal_1"))
        {
            manager.GetComponent<Gamemanager>().CreateSave();
            manager.GetComponent<Gamemanager>().SecondLevel();
        }
        else if (other.name.Equals("Goal_2"))
        {
            manager.GetComponent<Gamemanager>().WinLevel();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameObject manager = GameObject.Find("Game Manager");
            manager.GetComponent<Gamemanager>().RestartLevel(2);
        }
    }

    private void CheckIfFell ()
    {
        if (gameObject.transform.position.y < -10)
        {
            GameObject manager = GameObject.Find("Game Manager");
            manager.GetComponent<Gamemanager>().RestartLevel();
        }
    }

    private void ApplyGravity()
    {
        Rigidbody player = gameObject.GetComponent<Rigidbody>();
        player.AddForce(0, -0.3f, 0);
    }

    private void UpdateMovement()
    {
        //Movement by transforming position
        float movement = (10f * Time.deltaTime); //10 settled on by testing different speeds.

        if (Input.GetButton("Keyboard_W") || Input.GetAxis("Joystick_Vertical") < 0)
        {
            Vector3 vector = gameObject.transform.position;
            vector.z += movement;
            gameObject.transform.position = vector;
        }
        if (Input.GetButton("Keyboard_S") || Input.GetAxis("Joystick_Vertical") > 0)
        {
            Vector3 vector = gameObject.transform.position;
            vector.z -= movement;
            gameObject.transform.position = vector;
        }
        if (Input.GetButton("Keyboard_A") || Input.GetAxis("Joystick_Horizontal") < 0)
        {
            Vector3 vector = gameObject.transform.position;
            vector.x -= movement;
            gameObject.transform.position = vector;
        }
        if (Input.GetButton("Keyboard_D") || Input.GetAxis("Joystick_Horizontal") > 0)
        {
            Vector3 vector = gameObject.transform.position;
            vector.x += movement;
            gameObject.transform.position = vector;
        }
        if (Input.GetButton("Keyboard_Space") || Input.GetButton("Controller_Jump"))
        {
            Jump();
        }

    }

    private void UpdateCamera()
    {
        Vector3 position = viewCamera.transform.position;
        position.z = gameObject.transform.position.z - 10f;
        position.x = gameObject.transform.position.x;
        position.y = gameObject.transform.position.y + 3.6f;
        viewCamera.transform.position = position;
    }

    private void Jump()
    {
        if (inJump == true)
        {
            return;
        }
        Rigidbody player = gameObject.GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, 500f, 0);
        player.AddForce(force);
        inJump = true;
        preventJump = Time.time;
    }

    private void TestReadyToJump()
    {
        Rigidbody player = gameObject.GetComponent<Rigidbody>();
        Vector3 onGround = Vector3.zero;
        if (player.velocity == onGround && Time.time > preventJump + 1.0f)
        {
            inJump = false;
        }
    }
}
