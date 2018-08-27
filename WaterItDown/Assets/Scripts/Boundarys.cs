using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundarys : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
