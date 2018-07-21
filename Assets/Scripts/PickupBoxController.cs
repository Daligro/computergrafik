using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBoxController : MonoBehaviour {

    public float Rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rotationAngle = Rotation * Time.deltaTime;
        transform.Rotate(rotationAngle*0.6f, rotationAngle*1f, rotationAngle*0.3f, Space.World);
    }
}
