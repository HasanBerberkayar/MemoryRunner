using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public float startTime = 180f;
    public TMP_Text timerText;

    private float timeRemaining;
    private bool timerRunning = true;

    public GameObject mainMenu;
    public GameObject howToPlayMenu;

    public GameObject loseMenu;

    void Start()
    {
        timeRemaining = startTime;
        Time.timeScale = 0f;
        UpdateTimerText();
    }
    void Update()
    {
        if (!timerRunning)
            return;

        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timerRunning = false;
            OnTimerEnd();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text = "Remaining Time: " + minutes.ToString() + ":" + seconds.ToString("00");
    }

    void OnTimerEnd()
    {
        loseMenu.SetActive(true);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenHowToPlayMenu()
    {
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        howToPlayMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}