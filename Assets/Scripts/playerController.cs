using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;

    public AudioClip jumpSound;
    public AudioSource MusicSource;

    private bool canJump = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MusicSource.clip = jumpSound;
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = 0;

        if(Input.GetButton("Jump") && canJump)
        {
            jump = 1 * jumpHeight;
            MusicSource.Play();
        }

        Vector3 movement = new Vector3(moveHorizontal * speed, jump, moveVertical * speed);

        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision other)
    {
        print("test");
        if (other.gameObject.tag.Equals("Floor")) 
            canJump = true;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Equals("Floor"))
            canJump = false;
    }


}
