using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputFieldManager : MonoBehaviour {

    private InputField ipf;
	void Start ()
    {
        ipf = GetComponent<InputField>();
        ipf.onValueChanged.AddListener(delegate { SetName(); });
    }

    void SetName()
    {
        GameObject.Find("GameController").GetComponent<GameControl>().SetPlayerName(ipf.text);
    }
}
