using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupCollider : MonoBehaviour {

    public Material[] materials;
    public int respawnTime;
    public GameObject box;
    public AudioClip pickupSound;
    public AudioSource MusicSource;

    private float timePassed = 0;
    private bool respawned = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!box.activeSelf)
        {
            timePassed += Time.deltaTime;
            if(timePassed>=respawnTime)
            {
                timePassed = 0;
                box.SetActive(true);
                respawned = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        System.Random rnd = new System.Random();
        if (respawned)
        {
            switch (rnd.Next(0,materials.Length)) {
                case 0: activateSpeedball(other); break;

                case 1: activateJumpball(other); break;

                default: break;
            }
        }
    }

    private void activateSpeedball(Collider other)
    {
        MusicSource.clip = pickupSound;
        MusicSource.Play();
        box.SetActive(false);
        respawned = false;
        other.gameObject.GetComponent<Renderer>().material = materials[0];
        other.GetComponent<playerController>().materialChanged = true;
        other.GetComponent<playerController>().speed *= 3f;
    }

    private void activateJumpball(Collider other)
    {
        MusicSource.clip = pickupSound;
        MusicSource.Play();
        box.SetActive(false);
        respawned = false;
        other.gameObject.GetComponent<Renderer>().material = materials[1];
        other.GetComponent<playerController>().materialChanged = true;
        other.GetComponent<playerController>().jumpHeight *= 3f;
    }
}
