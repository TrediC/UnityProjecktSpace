using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundarys : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Loot") || other.gameObject.CompareTag("Obstacle"))
            other.gameObject.SetActive(false);
    }
}
