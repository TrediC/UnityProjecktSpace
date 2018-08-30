using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    private ObjectPooler op;
    private GameControl gc;

    [Header ("Player Properties")]
    public float speed;

    private Vector3 pos;

    private int taps;
    private float timer;

    void Start ()
    {
        gc = GameObject.Find("GameController").GetComponent<GameControl>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(gc == null)
        {
            gc = GameObject.Find("GameController").GetComponent<GameControl>();
            rb = GetComponent<Rigidbody>();
        }
    }
    void FixedUpdate () {
        MovePlayer();
        DoubleTap();
    }

    private void DoubleTap()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            gc.SaveScore();
            gc.ResetScore();
        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                taps += 1;
            }

            if (taps == 1)
            {
                timer = Time.time + 0.5f;
            }
            else if (taps == 10 && Time.time <= timer)
            {
                gc.SaveScore();
                GameObject.Find("GameController").GetComponent<SceneLoader>().LoadMenu();
                taps = 0;
            }

        }
        if (Time.time > timer)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Fish"))
        {
            gc.ResetScore();
            gc.GetComponent<SceneLoader>().LoadMenu();
        }

        if (other.gameObject.CompareTag("Loot"))
        {
            DestroyGameObject(other.gameObject);
        }
    }

    private void DestroyGameObject(GameObject obj)
    {
        if (obj.CompareTag("Loot"))
        {
            gc.Score = obj.GetComponent<Obstacle>().scoreAmount;
            speed *= 0.85f;
        }
        obj.SetActive(false);
    }
}
