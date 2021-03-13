using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RetryMenu : MonoBehaviour
{
    public PlayerController playerHealth;
    public TMP_Text enemyCount;
    public int enemyToDefeat = 5;
    public Tower towerHealth;
    public Image retryImage;
    public Image winImage;

    private bool _isRetryMenuActive = false;
    private bool _isWinMenuActive = false;
    private bool _isMenuActive = false;

    private void Start()
    {
        enemyCount.text = "Enemies to defeat: " + enemyToDefeat.ToString();
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
    }

    void ShowWinMenu()
    {
        if (enemyToDefeat <= 0)
        {
            winImage.gameObject.SetActive(true);
            playerHealth.enabled = false;
            _isWinMenuActive = true;
            _isMenuActive = true;
        }
    }

    void ShowRetryMenu()
    {
        if (playerHealth.currentHealth <= 0 || towerHealth.currentTowerHealth <= 0)
        {
            retryImage.gameObject.SetActive(true);
            _isRetryMenuActive = true;
            _isMenuActive = true;
        }
    }

    public void EnemyCounter()
    {
        enemyToDefeat--;
        if (enemyToDefeat >= 0)
        {
            enemyCount.text = "Enemies to defeat: " + enemyToDefeat.ToString();
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
