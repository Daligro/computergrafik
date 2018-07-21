using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public GameObject player;
    public bool rotateAroundPlayer = true;
    public float smoothFactor = 1;
    public float RotationSpeed = 1.5f;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {


        if(rotateAroundPlayer)
        {
            Quaternion camTurnAngle1 = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, -player.GetComponent<playerController>().gravityDirection);
            Quaternion camTurnAngle2 = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationSpeed, -player.transform.right);
            offset = camTurnAngle1 * camTurnAngle2 * offset;
        }

        Vector3 newPos = player.transform.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        transform.LookAt(player.transform);

	}
}
