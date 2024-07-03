using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ToGame()
    {
        SceneManager.LoadScene("02_Game");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("01_Title");
    }

    public void ToCustomScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
