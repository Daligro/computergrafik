using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraController : MonoBehaviour {

    /**
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
    **/
    public GameObject player;
    float distance = 10.0f;

    [Range(1,250)]
    public int xSpeed = 250;
    [Range(1, 300)]
    public int ySpeed = 120;

    int yMinLimit = -20;
    int yMaxLimit = 80;

    int distanceMin = 10;
    int distanceMax = 10;

    private float x = 0.0f;
    private float y = 0.0f;


    //@script AddComponentMenu("Camera-Control/Mouse Orbit")


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (player.GetComponent<Rigidbody>())
            player.GetComponent<Rigidbody>().freezeRotation = false;
    }

    void LateUpdate()
    {

        if (player.transform)
        {

            if (Input.GetKey("mouse 1"))
            {
                Cursor.visible = false;
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
            else
            {
                Cursor.visible = true;
            }
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation;

            if (!player.GetComponent<playerController>().gravityChanged)
                rotation = Quaternion.Euler(y, x, 0);
            else
                rotation = Quaternion.Euler(-x, y, 90f);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(player.transform.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }

            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + player.transform.position;

            transform.rotation = rotation;
            transform.position = position;

        }
    }


    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
