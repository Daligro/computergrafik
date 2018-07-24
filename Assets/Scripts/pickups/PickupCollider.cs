using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PickupCollider : MonoBehaviour {

    public Material[] materials;
    public Material defaultMaterial;
    public int respawnTime;
    public GameObject box;
    public AudioClip pickupSound;
    public AudioSource MusicSource;
    public Text powerUpText;
    public int enemiesDisabledTime;

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
        if (respawned && other.tag=="Player")
        {
            System.Random rnd = new System.Random();
            switch (rnd.Next(0,4)) {
                case 0: powerUpactivateSpeedball(other); break;

                case 1: powerUpactivateJumpball(other); break;

                case 2: powerUpactivateExtraLive(other); break;

                case 3: powerUpDisableEnemies(other); break;

                default: break;
            }
        }
    }

    private void powerUpactivateSpeedball(Collider other)
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

    private void powerUpactivateJumpball(Collider other)
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

    private void powerUpactivateExtraLive(Collider other)
    {
        if (other.GetComponent<PlayerController>().lives >= 0)
        {
            MusicSource.clip = pickupSound;
            MusicSource.Play();
            box.SetActive(false);
            respawned = false;
            other.GetComponent<PlayerController>().incrementLivesLeft();
            other.GetComponent<PlayerController>().updateLivesText();
            powerUpText.text = "1-UP";
            StartCoroutine(hidePowerUpText());
        }
        else
        {
            OnTriggerEnter(other);
        }
    }

    private void powerUpDisableEnemies(Collider other)
    {
        MusicSource.clip = pickupSound;
        MusicSource.Play();
        box.SetActive(false);
        respawned = false;
        other.gameObject.GetComponent<Renderer>().material = materials[2];
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        for(int i = 0; i<enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
        StartCoroutine(reactivateEnemies(enemies, other));

        powerUpText.text = "Enemies Disabled";
        StartCoroutine(hidePowerUpText());
    }

    private IEnumerator hidePowerUpText()
    {
        yield return new WaitForSeconds(2);
        powerUpText.text = "";
    }

    private IEnumerator reactivateEnemies(GameObject[] enemies, Collider other)
    {
        yield return new WaitForSeconds(enemiesDisabledTime);
        other.gameObject.GetComponent<Renderer>().material = defaultMaterial;

        for (int i = 0; i<enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
    }
}
