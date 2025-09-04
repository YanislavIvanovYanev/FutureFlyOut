using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, won, resume;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;       
        isPaused = false;
    }

    public void Won()
    {
        resume.SetActive(false);
        won.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;          
        isPaused = true;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Battle");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}
