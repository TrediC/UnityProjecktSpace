using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        GetComponent<GameControl>().currentState = GameState.PLAY;
    }

}
