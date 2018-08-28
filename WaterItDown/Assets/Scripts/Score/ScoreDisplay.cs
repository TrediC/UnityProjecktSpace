using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    private ScoreCalculate sc;
    int score;
    public Text scoreText;

    private void Start()
    {  
        sc = GetComponent<ScoreCalculate>();        
    }

    void Update()
    {
        score = sc.ShowScore();
        CanvasText();
    }

    public void CanvasText()
    {
        scoreText.text = "Score: " + score;
    }
}
