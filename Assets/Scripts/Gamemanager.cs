using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    // Used to restart level based on scene name.
    public void RestartLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        if (name.Equals("SecondLevel"))
        {
            RestartLevel(2);
        }
        else
        {
            RestartLevel(1);
        }
    }

    // Used to restart level based on passed-in integer.
    public void RestartLevel(int level)
    {
        if (level == 2)
        {
            SceneManager.LoadScene("SecondLevel");
        }
        else
        {
            SceneManager.LoadScene("FirstLevel");
        }

    }

    public void WinLevel()
    {
        SceneManager.LoadScene("WinLevel");
    }

    public void FirstLevel()
    {
        RestartLevel(1);
    }

    public void SecondLevel()
    {
        RestartLevel(2);
    }

    public void CreateSave()
    {
        PlayerPrefs.SetInt("LevelSave", 2);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("LevelSave");
    }

    public void LoadSave()
    {
        int save = PlayerPrefs.GetInt("LevelSave");
        if (save == 2)
        {
            SecondLevel();
        }
    }
}
