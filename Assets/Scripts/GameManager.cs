    using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int score = 0;
    private int initialScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameOver = false;
    private bool isGameWin = false;
    private bool isPause = false;
    void Start()
    {
        score = PlayerPrefs.GetInt("CurrentScore", 0);
        initialScore = score;
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
    }
    public void AddScore(int points)
    {
        if (!isGameOver&&!isGameWin)
        { 
            score+=points;
            UpdateScore();
        }
    }
    private void UpdateScore()
    {
        scoreText.text=score.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        score = initialScore;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    public void GameWin()
    {
        isGameWin = true;
        PlayerPrefs.SetInt("CurrentScore", score);
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public void RestartGame(int levelId)
    {
        isGameOver=false;
        isGameWin=false;
        score = initialScore;
        PlayerPrefs.SetInt("CurrentScore", score);
        UpdateScore();
        Time.timeScale = 1;
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel(int levelId)
    {
        if(levelId == 3)
        {
            SceneManager.LoadScene("Menu");
            PlayerPrefs.SetInt("UnlockedLevel", levelId);
            Time.timeScale = 1;
        }
        else
        {
            isGameOver=false;
            isGameWin=false;
            UpdateScore();
            Time.timeScale = 1;
            string levelName = "Level " + (levelId+1);
            PlayerPrefs.SetInt("UnlockedLevel", levelId);
            SceneManager.LoadScene(levelName);
        }
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0;
        pauseUi.SetActive(true);
    }
    public void Continue()
    {
        isPause = false;
        Time.timeScale = 1;
        pauseUi.SetActive(false);
    }
}
