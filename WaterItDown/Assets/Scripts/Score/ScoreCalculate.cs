using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ScoreCalculate : MonoBehaviour {

    public GameObject player;

    private int score = 0;
    private GameControl gc;
    private List<string> scores = new List<string>();
    private ScoreHolder scoreHolder = new ScoreHolder();

    void Start()
    {
        gc = gameObject.GetComponent<GameControl>();
        player = GameObject.Find("Player");
        scoreHolder = JsonUtility.FromJson<ScoreHolder>(File.ReadAllText(Application.persistentDataPath + "/HighScore.json"));
        AddScore();        
    }

    private void Update()
    {
        score = gc.Score;
    }

    void AddScore()
    {
        if(scoreHolder.HighScores != null)
        {
            scores = scoreHolder.HighScores;
            ScoreOrder();
        }

    }
    
    void ScoreOrder()
    {
        scores = scores.OrderByDescending(d => d).ToList();
        scores.RemoveAt(10);
        SaveScore();
    }

    public void NewScore()
    {
        string value = score + " " + gc.PlayerName;
        scores.Add(value);
        ScoreOrder();
    }

    public void SaveScore()
    {
        scoreHolder.HighScores = scores;
        string jsonData = JsonUtility.ToJson(scoreHolder, true);
        File.WriteAllText(Application.persistentDataPath + "/HighScore.json", jsonData);
        Debug.Log("Score saved");
    }

    public int ShowScore()
    {
        return score;
    }
    
}
