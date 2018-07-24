using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjust : MonoBehaviour {

    void FixedUpdate()
    {
        gameObject.GetComponent<AudioSource>().volume = ChangeVolume.volume;
    }
}
