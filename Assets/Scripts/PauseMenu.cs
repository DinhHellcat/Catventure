using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
    }
    public void Menu()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        SceneManagement.LoadScene("Main Menu");
    }
}
