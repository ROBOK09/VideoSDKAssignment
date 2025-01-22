using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> UpdateScore;

    private int score;

    private const int scoreIncrement = 10;

    private static ScoreManager instance;

    // ScoreManager (Singleton Pattern)

    public static ScoreManager Instance
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
        // Subscribing to various events fired on Game State Changes and update score when the player hits target (Pub - Sub Pattern)

        Enemy.hitTarget += SetScore;
        UIManager.Instance.GameStarted += SetInitialScore;
        UIManager.Instance.GameRestarted += SetInitialScore;
    }

    private void SetInitialScore()
    {
        score = 0;

        // Invoke Update Score Event intially on game started and game restarted to update the player score

        UpdateScore?.Invoke(score);
    }

    private void OnDisable()
    {
        // Desubscribing to various events fired on Game State Changes and update score when the player hits target to prevent memory leaks (Pub - Sub Pattern)

        Enemy.hitTarget -= SetScore;
        UIManager.Instance.GameStarted -= SetInitialScore;
        UIManager.Instance.GameRestarted -= SetInitialScore;
    }

    private void SetScore()
    {
        score += scoreIncrement;

        // Invoke Update Score Event when the user hits targets

        UpdateScore?.Invoke(score);
    }
}
