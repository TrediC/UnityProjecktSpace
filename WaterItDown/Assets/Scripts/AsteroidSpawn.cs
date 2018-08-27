using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour {

    public GameObject[] AsteroidList;  // list is for each asteroid type
    public ObjectPooler object_pooler;    //object pooler class use delegate
    private int index = 0;
    private void Start()
    {
        CreateAsteroids();
    }
    private void CreateAsteroids()
    {
        for(int i = 0; i < AsteroidList.Length; i++)
        {
            object_pooler = new ObjectPooler(PoolAsteroid, transform, 10, false);
            index++;
        }
    }
    private GameObject PoolAsteroid()
    {
        GameObject asteroid = Instantiate(AsteroidList[index], transform.position, Quaternion.identity);
        return asteroid;
    }

}