using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PickupCollider : MonoBehaviour {

    public Material[] materials;
    public int respawnTime;
    public GameObject box;
    public AudioClip pickupSound;
    public AudioSource MusicSource;
    public Text powerUpText;

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
            switch (rnd.Next(0,3)) {
                case 0: activateSpeedball(other); break;

                case 1: activateJumpball(other); break;

                case 2: activateExtraLive(other); break;

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
        other.GetComponent<PlayerController>().materialChanged = true;
        other.GetComponent<PlayerController>().speed *= 3f;
        powerUpText.text = "Speedball aktiviert";
        StartCoroutine(hidePowerUpText());
    }

    private void activateJumpball(Collider other)
    {
        MusicSource.clip = pickupSound;
        MusicSource.Play();
        box.SetActive(false);
        respawned = false;
        other.gameObject.GetComponent<Renderer>().material = materials[1];
        other.GetComponent<PlayerController>().materialChanged = true;
        other.GetComponent<PlayerController>().jumpHeight *= 3f;
        powerUpText.text = "Jumpball aktiviert";
        StartCoroutine(hidePowerUpText());
    }

    private void activateExtraLive(Collider other)
    {
        if (other.GetComponent<PlayerController>().lives < 0)
            OnTriggerEnter(other);
        MusicSource.clip = pickupSound;
        MusicSource.Play();
        box.SetActive(false);
        respawned = false;
        other.GetComponent<PlayerController>().lives++;
        other.GetComponent<PlayerController>().updateLivesText();
        powerUpText.text = "1-UP";
        StartCoroutine(hidePowerUpText());
    }

    private IEnumerator hidePowerUpText()
    {
        yield return new WaitForSeconds(2);
        powerUpText.text = "";
    }
}
