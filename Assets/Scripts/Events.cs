using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void replayGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("MainTheme");
        SceneManager.LoadScene("Level");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
