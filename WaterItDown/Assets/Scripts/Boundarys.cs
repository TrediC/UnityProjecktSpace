using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundarys : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Loot"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
