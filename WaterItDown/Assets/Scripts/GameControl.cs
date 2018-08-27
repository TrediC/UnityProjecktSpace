using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    [Header("Wave size and difirent asteroid types")]
    public int WaveSize = 5;
    public int AsteroidTypeCount;

    [SerializeField]
    private List<GameObject> asteroidList;
    [Header("Asteroid wave times and place")]
    public float waveStart = 5;
    public float spawnWait = 1;
    public float waveWait = 3;
    public float spawnValuesX;
    public float spawnValuesY;

    [SerializeField]
    private float offset = 5;

    [Header("Score")]
    private int mScore = 0;
    public int Score
    {
        get
        {
            return mScore;
        }
        set
        {
            mScore += value;
        }
    }
    void Start () {
    }
	
	void Update () {
        if (asteroidList.Count == 0)
            FindAllAsteroids();
    }

    public void FindAllAsteroids()
    {
        Transform[] trans = GameObject.Find("GameController").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
        {
            if (t.gameObject.CompareTag("Asteroid"))
            {
                asteroidList.Add(t.gameObject);
            }
        }
    }
    // return asteroids by name from list
    private GameObject GetAsteroidByType(string type)
    {
        for (int i = 0; i < asteroidList.Count; i++)
        {
            if (!asteroidList[i].activeInHierarchy)
            {
                if (asteroidList[i].GetComponent<Asteroid>().asteroidType.ToString() == type)
                {
                    asteroidList[i].gameObject.SetActive(true);
                    asteroidList[i].transform.position = new Vector3(UnityEngine.Random.Range(-spawnValuesX - offset, spawnValuesY), spawnValuesY, 0);
                    return asteroidList[i];
                }
            }
        }
        return null;
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(waveStart);
        while (true)
        {
            for (int i = 0; i < WaveSize; i++)
            {
                int selection = UnityEngine.Random.Range(1, AsteroidTypeCount);
                switch (selection)
                {
                    case 0:
                        GetAsteroidByType("OreAsteroid");
                        break;
                    case 1:
                        GetAsteroidByType("SmallAsteroid");
                        break;
                    case 2:
                        GetAsteroidByType("MediumAsteroid");
                        break;
                    case 3:
                        GetAsteroidByType("BigAsteroid");
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            GetAsteroidByType("OreAsteroid");
            for(int i = 0; i < asteroidList.Count; i++)
            {
                asteroidList[i].GetComponent<Asteroid>().speed++;
            }
        }
    }
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 60, 20), "Wave"))
            StartCoroutine(SpawnAsteroids());
        string scroreString = mScore.ToString();
        GUI.TextField(new Rect(10, 40, 60, 20), scroreString);
    }
    
}
