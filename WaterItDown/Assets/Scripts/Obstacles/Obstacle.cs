using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int scoreAmount;
    public ObstacleTypes obstacles;
    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Loot");
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
