using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    private ObjectPooler op;
    private GameControl gc;
    private float damage = 1;

    [Header ("Player Properties")]
    public float speed;
    public float health = 5;

    private Vector3 pos;

    private int taps;
    private float timer;

    void Start ()
    {
        gc = GameObject.Find("GameController").GetComponent<GameControl>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        MovePlayer();
        DoubleTap();
    }

    private void DoubleTap()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Double tap");
            ++taps;
        }
        if (taps > 0)
        {
            timer += Time.deltaTime;
        }
        if (taps >= 2)
        {
            //TODO:

            gc.GetComponent<ScoreCalculate>().NewScore();

            timer = 0.0f;
            taps = 0;
        }
        if (timer > 0.5f)
        {
            timer = 0f;
            taps = 0;
        }
    }

    private void MovePlayer()
    {
        
        if (Input.GetButton("Fire1"))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
        var dis = Vector3.Distance(pos, gameObject.transform.position);
        if (dis < 20.0f)
            rb.MovePosition(Vector3.Lerp(transform.position, pos, Time.deltaTime * speed));
        else
        {
            rb.MovePosition(Vector3.LerpUnclamped(transform.position, pos, Time.deltaTime * (speed * 0.5f)));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health -= damage;

            if(damage <= 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Loot"))
        {
            DestroyGameObject(collision.gameObject);
        }
    }

    private void DestroyGameObject(GameObject obj)
    {
        obj.SetActive(false);
        if (obj.CompareTag("Loot"))
        {
            gc.Score = obj.GetComponent<Obstacle>().scoreAmount;
            print("Score");
        }
    }
}
