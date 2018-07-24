using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timeText;

    private int currentSeconds=0;
    private int currentMinutes = 0;

	// Use this for initialization
	void Start ()
    {
        timeText.text = currentMinutes.ToString() + ":0" + (++currentSeconds).ToString();
        InvokeRepeating("IncreaseTime", 1, 1);
	}

    // Update is called once per frame
    void IncreaseTime()
    {
        if(currentSeconds>=59)
        {
            currentSeconds = 0;
            currentMinutes++;
        }
        if (currentSeconds < 9)
        {
            timeText.text = currentMinutes.ToString() + ":0" + (++currentSeconds).ToString();

        }
        else
        {
            timeText.text = currentMinutes.ToString() + ":" + (++currentSeconds).ToString();
        }
    }    

    public int getCurrentSeconds()
    {
        return currentSeconds;
    }

    public int getCurrentMinutes()
    {
        return currentMinutes;
    }
}
