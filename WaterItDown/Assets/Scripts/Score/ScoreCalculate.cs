using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ScoreCalculate : MonoBehaviour {

    private int score = 0;
    private GameControl gc;
    private List<string> scores = new List<string>();
    private ScoreHolder scoreHolder = new ScoreHolder();

    public void Start()
    {
        gc = gameObject.GetComponent<GameControl>();
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
        string scoreString = "0";
        var temp = score.ToString().Length;
        for (int i = 0; i < (6-temp); ++i)
        {
            scoreString += "0";
        }
        string value = scoreString + score + " " + gc.PlayerName;
        scores.Add(value);
        ScoreOrder();
    }

    public void SaveScore()
    {
        scoreHolder.HighScores = scores;
        string jsonData = JsonUtility.ToJson(scoreHolder, true);
        File.WriteAllText(Application.persistentDataPath + "/HighScore.json", jsonData);
        Debug.Log("Score saved");

        foreach(string x in scoreHolder.HighScores)
        {
            Debug.Log(x + "\n");
        }
    }

    public int ShowScore()
    {
        return score;
    }
    
}
