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

    [Header("Bullets")]
    public GameObject Bullet;
    public Transform AmmoPool;
    private float fireRateTimer;

    private Vector3 pos;


    void Start () {
        rb = GetComponent<Rigidbody>();
        op = new ObjectPooler(CreateBulletObject, AmmoPool, 20, false);
    }

    public GameObject CreateBulletObject()
    {
        GameObject obj = Instantiate(Bullet);
        return obj;
    }


    void FixedUpdate () {
        MovePlayer();
        //Shoot();
    }

    private void MovePlayer()
    {
        if (Input.GetButton("Fire1"))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
        rb.MovePosition(Vector3.Lerp(transform.position, pos, Time.deltaTime * speed));
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > fireRateTimer)
        {
            GameObject bullet = op.GetObject();
            
            if (bullet == null)
                return;
            else
            {
                bullet.transform.position = this.transform.position;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = Vector2.up * bullet.GetComponent<Bullet>().BulletSpeed;
                StartCoroutine(BulletLifeTime(bullet));
            }
            fireRateTimer = Time.time + Bullet.GetComponent<Bullet>().FireRate;
        }
    }
    IEnumerator BulletLifeTime(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        bullet.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            health -= damage;

            if(damage <= 0)
                Destroy(gameObject);
        }
    }
}
