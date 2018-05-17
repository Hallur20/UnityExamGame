using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinButtonBehaviour : MonoBehaviour {
    public Button button;
    // Use this for initialization
    void Start () {
        button.onClick.AddListener(GoToNextLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void GoToNextLevel() {
        SceneManager.LoadScene("_scene2");
    }
}
