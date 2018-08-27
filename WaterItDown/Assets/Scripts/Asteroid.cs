using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour, IAsteroid  {

    private GameControl gameControl;
    
    public int Health {
        get
        {
            return _health;
        }
        set
        {
            _health -= value;
            if (_health <= 0)
            {
                gameObject.SetActive(false);
                gameControl.Score = 1 + (int)asteroidType;
            }
        }
    }

    public AsteroidType asteroidType;
    
    private int _health = 1;
    public float speed = 5;
    public Rigidbody _rigidbody;

    public Vector3 m_EulerAngleVelocity;
    private float randomRotationX;
    private float randomRotationY;
    private float randomRotationZ;
    private void Start()
    {
        _health += (int)asteroidType;
        gameControl = GameObject.Find("GameController").GetComponent<GameControl>();
        randomRotationX = RandomRotation(randomRotationX);
        randomRotationY = RandomRotation(randomRotationY);
        randomRotationZ = RandomRotation(randomRotationZ);
        m_EulerAngleVelocity = new Vector3(randomRotationX, randomRotationY, randomRotationZ);
        _rigidbody.AddForce(Vector3.down * speed, ForceMode.Impulse);
    }
    private void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }

    private float RandomRotation(float rand)
    {
        rand = UnityEngine.Random.Range(-50, 50);

        return rand;
    }
}
