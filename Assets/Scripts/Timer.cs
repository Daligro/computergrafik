using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GUIText timeText;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("IncreaseTime", 1, 1);
	}

    // Update is called once per frame
    void IncreaseTime()
    {
        int currentTime = int.Parse(timeText.text) + 1;
        timeText.text = currentTime.ToString();

    }    
}
