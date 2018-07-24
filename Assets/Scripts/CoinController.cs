using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        int maxCoins = 0;
        maxCoins += transform.childCount;
        player.GetComponent<PlayerController>().setMaxCoins(maxCoins);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
