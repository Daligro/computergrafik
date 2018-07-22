using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneController : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().instantDeath();
    }
}
