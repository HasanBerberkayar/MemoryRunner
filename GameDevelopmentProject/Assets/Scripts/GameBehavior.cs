using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    /*
    public int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    private void Start()
    {
        ItemText.text += _itemCollected;
        HealthText.text += _playerHP;
    }

    private int _itemCollected = 0;
    public int Items
    {
        get { return _itemCollected; }
        set
        {
            _itemCollected = value;
            ItemText.text = "Items: " + Items;
            if (_itemCollected >= MaxItems)
            {
                ProgressText.text = "You have found all the items!";
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemCollected) + " more!";
            }
        }
    }

    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            HealthText.text = "Health: " + HP;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }*/

    public float startTime = 180f;
    public TMP_Text timerText;

    private float timeRemaining;
    private bool timerRunning = true;

    public GameObject mainMenu;
    public GameObject howToPlayMenu;

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
        Debug.Log("Time's up!");
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