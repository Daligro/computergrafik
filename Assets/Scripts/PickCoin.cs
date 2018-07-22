using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCoin : MonoBehaviour {

    public AudioClip coinSound;
    //public Text coinScore;
    
    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        //int score = int.Parse (coinScore.text) + 1;
        //coinScore.text = score.ToString();
        Destroy(gameObject);
    }
}
