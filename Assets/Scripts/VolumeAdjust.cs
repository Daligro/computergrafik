using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjust : MonoBehaviour {

    AudioSource source;
    ChangeVolume audio = new ChangeVolume();
	// Use this for initialization
	void Start () {
        source = audio.volumeAudio;
        source.volume = audio.volumeAudio.volume;
    }
}
