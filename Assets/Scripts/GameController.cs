using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public PlayerController playerObject;
    public Tower towerObject;
    public TMP_Text enemyCountText;
    public Image retryImage;
    public Image winImage;
    public int enemyToDefeat = 5;

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
    }

    void ShowWinMenu()
    {
        if (enemyToDefeat <= 0)
        {
            winImage.gameObject.SetActive(true);
            playerObject.enabled = false;
            _isWinMenuActive = true;
            _isMenuActive = true;
        }
    }

    void ShowRetryMenu()
    {
        if (playerObject.currentHealth <= 0 || towerObject.currentTowerHealth <= 0)
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
            enemyCountText.text = "Enemies to defeat: " + enemyToDefeat.ToString();
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
