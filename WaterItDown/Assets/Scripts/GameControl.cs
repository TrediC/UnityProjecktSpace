using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour 
{
    public string PlayerName;
    public GameState currentState;

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
                Exit();
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

    private GameObject GetObstaclesByType(ObstacleTypes type)
    {
        for (int i = 0; i < obstacleList.Count; i++)
        {
            if (!obstacleList[i].activeInHierarchy)
            {
                if (obstacleList[i].GetComponent<Obstacle>().obstacles == type)
                {
                    obstacleList[i].gameObject.SetActive(true);
                    obstacleList[i].transform.position = new Vector3(UnityEngine.Random.Range(-spawnValuesX - offset, spawnValuesY), spawnValuesY, 0);
                    return obstacleList[i];
                }
            }
        }
        return null;
    }

    private void FixedUpdate()
    {
        print(currentState);

        if (obstacleList == null || obstacleList.Count == 0)
            FindAllObstacles();
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(waveStart);
        while (true)
        {
            for (int i = 0; i < WaveSize; i++)
            {
                int selection = UnityEngine.Random.Range(0, Obstacles.Length);
                switch (selection)
                {
                    case 0:
                        GetObstaclesByType(ObstacleTypes.Weed);
                        break;
                    case 1:
                        GetObstaclesByType(ObstacleTypes.Rock);
                        break;
                    case 2:
                        GetObstaclesByType(ObstacleTypes.Coral);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(spawnWait);
            }
            GetObstaclesByType(ObstacleTypes.Loot);
            yield return new WaitForSeconds(waveWait);
        }
    }
    void OneSecUpdate()
    {
       
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void SetState(GameState state)
    {
        currentState = state;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        currentState = GameState.PLAY;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
