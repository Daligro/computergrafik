using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCoin : MonoBehaviour {

    public AudioClip coinSound;
    public GUIText coinText;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        int score = int.Parse (coinText.text) + 1;
        coinText.text = score.ToString();
        Destroy(gameObject);
    }
}
