using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Events being fired on GameState changes (Pub - Sub Pattern)

    public event Action GameStarted;
    public event Action GameOver;
    public event Action GamePaused;
    public event Action GameResumed;
    public event Action GameRestarted;

    [SerializeField]
    private TextMeshProUGUI scoreGameplay;

    [SerializeField]
    private TextMeshProUGUI healthGameplay;

    [SerializeField]
    private TextMeshProUGUI scoreGameOver;

    [SerializeField]
    private GameObject gameStartPanel;
    
    [SerializeField]
    private GameObject gamePlayPanel;
    
    [SerializeField]
    private GameObject gamePausePanel;

    [SerializeField]
    private GameObject gameOverPanel;

    private GameState currentGameState = GameState.GAMESTARTED;
    private int currentScore;
    private static UIManager instance;

    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }
        private set
        {
            currentGameState = value;
        }
    }

    // UIManager (Singleton Pattern)

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Subscribing to events fired on score and health updates (Pub - Sub Pattern)

        HealthManager.Instance.UpdateHealth += UpdateHealthText;
        ScoreManager.Instance.UpdateScore += UpdateScoreText;
    }

    // Method fired when player clicks on Play Button

    public void OnClickPlay()
    {
        gameStartPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        currentGameState = GameState.INGAME;
        GameStarted?.Invoke();
    }

    // Method fired when player clicks on Restart Button

    public void OnClickRestart()
    {
        if (CurrentGameState == GameState.GAMEPAUSED)
        {
            gamePausePanel.SetActive(false);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
        Time.timeScale = 1;
        gamePlayPanel.SetActive(true);
        currentGameState = GameState.INGAME;
        GameRestarted?.Invoke();
    }

    // Method fired when player clicks on Quit Button

    public void OnClickQuit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }

    // Method fired when player clicks on Resume Button

    public void OnClickResume()
    {
        gamePlayPanel.SetActive(true);
        gamePausePanel.SetActive(false);
        Time.timeScale = 1;
        currentGameState = GameState.INGAME;
        GameResumed?.Invoke();
    }

    // Method fired when player clicks on Pause Button

    private void OnClickPause()
    {
        gamePlayPanel.SetActive(false);
        gamePausePanel.SetActive(true);
        Time.timeScale = 0;
        currentGameState = GameState.GAMEPAUSED;
        GamePaused?.Invoke();
    }

    private void Update()
    {
        if (CurrentGameState != GameState.GAMESTARTED && CurrentGameState != GameState.GAMEOVER)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Pause or Resume the game on the Escape key if the user is not in the gamestart or the gameover state 

                if (CurrentGameState == GameState.GAMEPAUSED)
                {
                    OnClickResume();
                }
                else
                {
                    OnClickPause();
                }
            }
        }
    }

    private void OnDisable()
    {
        // Subscribing to events fired on score and health updates to prevent memory leaks (Pub - Sub Pattern)

        HealthManager.Instance.UpdateHealth -= UpdateHealthText;
        ScoreManager.Instance.UpdateScore -= UpdateScoreText;
    }

    // Update Score when the score update event is fired

    private void UpdateScoreText(int scoreValue)
    {
        scoreGameplay.text = "SCORE :  " + scoreValue;
        currentScore = scoreValue;
    }

    // Update Health when the health update event is fired

    private void UpdateHealthText(int healthValue)
    {
        healthGameplay.text = "HEALTH :  " + healthValue;

        if (healthValue <= 0)
        {
            // Check if health is less than 0 and fire the game over event

            scoreGameOver.text = "SCORE :  " + currentScore;
            gamePlayPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            currentGameState = GameState.GAMEOVER;
            GameOver?.Invoke();
        }
    }
}
