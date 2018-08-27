using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public ObjectPooler object_pooler;    //object pooler class use delegate
    public int amount;
    private GameControl gc;
    private int index = 0;
    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameControl>();
        CreateObjects();
    }
    private void CreateObjects()
    {
        for (int i = 0; i < gc.Obstacles.Length; i++)
        {
            object_pooler = new ObjectPooler(PoolObstacles, transform, amount, false);
            index++;
        }
    }
    private GameObject PoolObstacles()
    {
        return Instantiate(gc.Obstacles[index], transform.position, Quaternion.identity);
    }
}
