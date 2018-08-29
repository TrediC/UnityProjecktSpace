using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour 
{
    public string PlayerName;
    public GameState currentState;
    public InputField inputName;

    [Header("Wave size and difirent obstacles types")]
    public int WaveSize = 5;
    public GameObject[] Obstacles;

    [SerializeField]
    private List<GameObject> obstacleList;
    private IObstacles obstacleTypes;

    [Header("Obstacle wave times and place")]
    public float waveStart = 5;
    public float spawnWait = 1;
    public float waveWait = 3;
    public float spawnValuesX;
    public float spawnValuesY;

    [SerializeField]
    private float offset = 5;
    private float oneSecUpdate;

    private int _score = 0;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score += value;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start ()
    {

    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.PLAY)
                currentState = GameState.PAUSE;

            else if (currentState == GameState.PAUSE)
                currentState = GameState.RESUME;
        }

        if(Time.time > oneSecUpdate)
        {
            oneSecUpdate = 1.0f + Time.time;
            OneSecUpdate();
        }

        switch (currentState)
        {
            case GameState.MENU:
                break;
            case GameState.PLAY:
                StartCoroutine(SpawnObstacles());
                break;
            case GameState.PAUSE:
                PauseGame();
                break;
            case GameState.RESUME:
                ResumeGame();
                break;
            case GameState.RESTART:
                break;
            case GameState.EXIT:
                break;
            default:
                break;
        }
    }

    public void FindAllObstacles()
    {
        foreach (Transform child in transform)
        {
            obstacleList.Add(child.gameObject);
        }
    }

    GameObject ObstaclesByType(ObstacleTypes type)
    {
        for (int i = 0; i < obstacleList.Count; ++i)
        {
            if(obstacleList[i].GetComponent<Obstacle>().obstacles == type)
            {
                if(!obstacleList[i].activeInHierarchy)
                {
                    obstacleList[i].gameObject.SetActive(true);
                    return obstacleList[i].gameObject;
                }
            }
        }
        return null;
    }

    private void FixedUpdate()
    {
        if (obstacleList == null || obstacleList.Count == 0)
            FindAllObstacles();
    }

    IEnumerator SpawnObstacles()
    {
        GameObject obj = null;
        GameObject loot = null;
        Vector3 spawnPosition;
        yield return new WaitForSeconds(waveStart);
        for (int i = 0; i < WaveSize; i++)
        {
            int selection = UnityEngine.Random.Range(0, Obstacles.Length);
            switch (selection)
            {
                case 0:
                    obj = ObstaclesByType(ObstacleTypes.Weed);
                    break;
                case 1:
                    ObstaclesByType(ObstacleTypes.Rock);
                    break;
                case 2:
                    ObstaclesByType(ObstacleTypes.Coral);
                    break;
                default:
                    break;
            }
            spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValuesX, spawnValuesX), UnityEngine.Random.Range(spawnValuesY, spawnValuesY * 2), 0.0f);
            if(obj != null)
                obj.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
        }
        yield return new WaitForSeconds(waveWait);
        loot = ObstaclesByType(ObstacleTypes.Loot);
        spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValuesX, spawnValuesX), UnityEngine.Random.Range(spawnValuesY, spawnValuesY * 2), 0.0f);
        if (loot != null)
            loot.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
    }
    void OneSecUpdate()
    {
        

    }

    public void SetPlayerName()
    {
        PlayerName = inputName.text;
    }
    public void ResetScore()
    {
        _score = 0;
    }

    void PauseGame()
    {
        GameObject.Find("PauseMenu").SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        currentState = GameState.PLAY;
    }
    public void ResumeGame()
    {
        GameObject.Find("PauseMenu").SetActive(false);
        Time.timeScale = 1;
        currentState = GameState.PLAY;
    }
}

public enum GameState
{
    MENU,
    PLAY,
    PAUSE,
    RESUME,
    RESTART,
    EXIT
}
