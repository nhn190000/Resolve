using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
    
