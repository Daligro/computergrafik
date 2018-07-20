using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    [Range(0.01f,10.0f)]
    public float speed;
    [Range(0.1f,1000.0f)]
    public float jumpHeight;
    [Range(0.1f,10.0f)]
    public float friction;

    public GameObject mainCamera;
    public AudioClip jumpSound;
    public AudioSource MusicSource;

    private bool canJump = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MusicSource.clip = jumpSound;
        rb.angularDrag = friction;
    }
    void FixedUpdate()
    {
        Vector3 fromCameraToMe = transform.position - mainCamera.transform.position;
        fromCameraToMe.y = 0;
        fromCameraToMe.Normalize();
        //Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //float force = speed * movementVector.magnitude;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float jump = 0;

        if(canJump)
        {
            rb.angularDrag = friction;
        }
        else
        {
            rb.angularDrag = 0.1f;
        }

        if (Input.GetButton("Jump") && canJump)
        {
            jump = 1 * jumpHeight;
            MusicSource.Play();
        }
        Vector3 movement = (fromCameraToMe * moveVertical + mainCamera.transform.right * moveHorizontal)*speed;
        movement.y += jump;
        //Vector3 direction = movementVector.y *  + jump + movementVector.x * maincam.;
        //Vector3 movement = new Vector3(moveHorizontal * speed, jump, moveVertical * speed);

        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision other)
    {
        print("jump");
        if (other.gameObject.tag.Equals("Floor")) 
            canJump = true;
    }

    void OnCollisionExit(Collision other)
    {
        print("land");
        if (other.gameObject.tag.Equals("Floor"))
            canJump = false;
    }


}
