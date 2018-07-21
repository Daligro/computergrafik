﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    [Range(0.01f, 30.0f)]
    public float speed;
    [Range(0.1f, 1000.0f)]
    public float jumpHeight;
    [Range(0.1f, 10.0f)]
    public float friction;
    [Range(0.01f, 1f)]
    public float volumeSpeedScale;
    [Range(0.1f, 20f)]
    public float gravity;
    public Vector3 gravityDirection;

    public int powerUpTime;
    public GameObject mainCamera;
    public AudioClip jumpSound;
    public AudioClip bounceSound;
    public AudioSource MusicSource;
    public Material normalMaterial;
    public bool materialChanged = false;
    public bool directionChanged = false;
    public bool gravityChanged = false;

    private float speedIfChanged;
    private float jumpHeightIfChanged;
    private float frictionIfChanged;

    private bool canJump = true;
    private Rigidbody rb;

    private float timePassed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = friction;
        speedIfChanged = speed;
        jumpHeightIfChanged = jumpHeight;
        frictionIfChanged = friction;
    }
    void FixedUpdate()
    {
        //wenn das material der Kugel verändert wurde:
        if (materialChanged)
        {
            //Zeit messen
            timePassed += Time.deltaTime;
            if (timePassed >= powerUpTime)
            {
                //material + timer zurücksetzen
                deactivatePowerUp();
            }
        }

        if (Input.GetKey("t"))
        { 
            //TODO: Gravitation drehen implementieren
            //gravitation
            //rb.velocity.y += gravity * Time.deltaTime;
            gravityDirection = new Vector3(9.81f, 0f, 0f);
            Physics.gravity = gravityDirection;
            gravityChanged = true;
            //mainCamera.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);
        }else{
            gravityDirection = new Vector3(0f, -9.81f, 0f);
            Physics.gravity = gravityDirection;
            gravityChanged = false;
        }
        

        Vector3 fromCameraToMe = transform.position - mainCamera.transform.position;
        fromCameraToMe.y = 0;
        fromCameraToMe.Normalize();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(canJump)
        {
            rb.angularDrag = friction;
        }
        else
        {
            rb.angularDrag = 0.1f;
        }


        Vector3 movement = (fromCameraToMe * moveVertical + mainCamera.transform.right * moveHorizontal) * speed;

        rb.AddForce(movement);
    }

    //contains: jump
    void LateUpdate()
    {
        if (canJump)

        if (Input.GetButton("Jump") && canJump)
        {
            jump();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        float speed = rb.velocity.magnitude;
        MusicSource.clip = bounceSound;
        MusicSource.volume = volumeSpeedScale * speed;
        MusicSource.Play();
        if (other.gameObject.tag.Equals("Floor")) {
            if (Input.GetButton("Jump"))
                jump();
            else
                canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Equals("Floor"))
            canJump = false;
    }

    private void deactivatePowerUp()
    {
        this.gameObject.GetComponent<Renderer>().material = normalMaterial;
        timePassed = 0f;
        materialChanged = false;
        speed = speedIfChanged;
        jumpHeight = jumpHeightIfChanged;
        friction = frictionIfChanged;
    }

    private void jump()
    {
        MusicSource.clip = jumpSound;
        MusicSource.volume = 0.8f;
        MusicSource.Play();
        Vector3 jumpDirection = -Vector3.Normalize(gravityDirection);
        Vector3 movement = jumpDirection*jumpHeight;
        rb.AddForce(movement);
    }
}
