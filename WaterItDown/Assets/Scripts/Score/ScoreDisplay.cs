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

    void FixedUpdate()
    {
        score = sc.ShowScore();

        if (scoreText != null)
        {
            CanvasText();
            
        }
        else
        {
            if (GetComponent<GameControl>().currentState == GameState.PLAY)
                scoreText = GameObject.Find("ScoreField").GetComponent<Text>();
        }
    }

    public void CanvasText()
    {
        scoreText.text = "Score: " + score;
    }
}
