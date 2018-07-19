using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // This is a reference to the Rigidbody component called "rb"
    public Rigidbody rb;
	
	// We marked this as "FixedUpdate" because we
    // are using it to mess with physics.
	void FixedUpdate ()
    {
        rb.AddForce(0, 0, 2000 * Time.deltaTime);
	}
}
