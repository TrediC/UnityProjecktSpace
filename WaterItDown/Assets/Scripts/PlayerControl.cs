using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    private ObjectPooler op;
    private float damage = 1;

    [Header ("Player Properties")]
    public float speed;
    public float health = 5;

    private Vector3 pos;


    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetButton("Fire1"))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
        rb.MovePosition(Vector3.Lerp(transform.position, pos, Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health -= damage;

            if(damage <= 0)
                Destroy(gameObject);
        }
    }
}
