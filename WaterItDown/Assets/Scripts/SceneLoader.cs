using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel()
    {
        GameObject.Find("GameController").GetComponent<GameControl>().StartGame();
        SceneManager.LoadScene(2);
    }

}
