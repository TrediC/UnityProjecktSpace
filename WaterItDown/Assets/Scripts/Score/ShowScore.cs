using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    public Text scoreField;
    List<string> scores = new List<string>();
    ScoreHolder scoreHolder = new ScoreHolder();

    void Start () {
        scoreHolder = JsonUtility.FromJson<ScoreHolder>(File.ReadAllText(Application.persistentDataPath + "/HighScore.json"));
        scores = scoreHolder.HighScores;
        ShowScores();
    }
	
	void Update () {       

    }
    void ShowScores()
    {
        int i = 1;
        foreach (string score in scores)
        {
            scoreField.text = i + ". " + score + "\n";
            ++i;
        }
    }
}
