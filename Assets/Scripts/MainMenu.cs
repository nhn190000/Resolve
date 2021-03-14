using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image options;
    public Image menu;
    public Image howToPlay;
    public void StartGameButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OptionsButton()
    {
        menu.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
    }

    public void BackButton()
    {
        options.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void HowToPlay()
    {
        menu.gameObject.SetActive(false);
        howToPlay.gameObject.SetActive(true);
    }

    public void HowToPlayBackButton()
    {
        howToPlay.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}