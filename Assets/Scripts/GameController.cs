using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public PlayerController playerObject;
    public Tower towerObject;
    public TMP_Text enemyCountText;
    public Image retryMenu;
    public Image winMenu;
    public Image pauseMenu;
    public int enemyToDefeat = 5;

    public static bool GameIsPaused = false;

    private bool _isRetryMenuActive = false;
    private bool _isWinMenuActive = false;
    private bool _isMenuActive = false;

    private void Start()
    {
        enemyCountText.text = "Enemies to defeat: " + enemyToDefeat.ToString();
    }

    void Update()
    {
        if (!_isRetryMenuActive && !_isMenuActive)
        {
            ShowRetryMenu();
        }
        if (!_isWinMenuActive && !_isMenuActive)
        {
            ShowWinMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_isMenuActive)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void ShowWinMenu()
    {
        if (enemyToDefeat <= 0)
        {
            winMenu.gameObject.SetActive(true);
            playerObject.enabled = false;
            _isWinMenuActive = true;
            _isMenuActive = true;
        }
    }

    void ShowRetryMenu()
    {
        if (playerObject.currentHealth <= 0 || towerObject.currentTowerHealth <= 0)
        {
            retryMenu.gameObject.SetActive(true);
            _isRetryMenuActive = true;
            _isMenuActive = true;
        }
    }

    public void EnemyCounter()
    {
        enemyToDefeat--;
        if (enemyToDefeat >= 0)
        {
            enemyCountText.text = "Enemies to defeat: " + enemyToDefeat.ToString();
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
