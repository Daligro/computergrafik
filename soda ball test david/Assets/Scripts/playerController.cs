using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = 0;

        if(Input.GetButtonDown("Jump"))
        {
            jump = 1 * jumpHeight;
        }

        Vector3 movement = new Vector3(moveHorizontal * speed, jump, moveVertical * speed);

        rb.AddForce(movement);
    }


 }
