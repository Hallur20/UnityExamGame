using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinButtonBehaviour : MonoBehaviour
{
    public Button button;
    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(GoToNextLevel);
    }


    private void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("_scene"))
        {
            SceneManager.LoadScene("_scene2");
        }
        if (SceneManager.GetActiveScene().name.Equals("_scene2"))
        {
            SceneManager.LoadScene("_scene3");
        }
    }
}