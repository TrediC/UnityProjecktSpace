using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet {
    public int damage = 1;
    public BulletType bulletType;
    [Header("Bullet Propeties")]
    public float laserTmer = 2;
    public float explosionArea = 2;
    public float BulletSpeed = 5;
    public float FireRate = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            other.gameObject.GetComponent<Asteroid>().Health = damage;
        }
        else { return; }
        switch (bulletType)
        {
            case BulletType.NormalBullet:
                gameObject.SetActive(false);
                break;
            case BulletType.LaserBuller:
                StartCoroutine(LaserBullet());
                break;
            case BulletType.MissileBullet:
                break;
            default:
                break;
        }

    }

    IEnumerator LaserBullet()
    {
        yield return new WaitForSeconds(laserTmer);
        gameObject.SetActive(false);
    }
    
    void MissileExplosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, explosionArea);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Asteroid"))
            {
                hitColliders[i].GetComponent<Asteroid>().Health = damage;
                gameObject.SetActive(false);
            }
            i++;
        }
    }
}