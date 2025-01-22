using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public event Action<int> UpdateHealth;

    private int health;

    private const int healthDecrement = 5;

    private static HealthManager instance;

    // HealthManager (Singleton Pattern)

    public static HealthManager Instance
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
        // Subscribing to various events fired on Game State Changes and update health when the player misses target (Pub - Sub Pattern)

        Enemy.missTarget += SetHealth;
        UIManager.Instance.GameStarted += SetInitialHealth;
        UIManager.Instance.GameRestarted += SetInitialHealth;
    }

    private void SetInitialHealth()
    {
        health = 100;

        // Invoke Update Health Event intially on game started and game restarted to update the player health

        UpdateHealth?.Invoke(health);
    }

    private void OnDisable()
    {
        // Desubscribing to various events fired on Game State Changes and update health when the player misses target to prevent memory leaks (Pub - Sub Pattern)

        Enemy.missTarget -= SetHealth;
        UIManager.Instance.GameStarted -= SetInitialHealth;
        UIManager.Instance.GameRestarted -= SetInitialHealth;
    }

    private void SetHealth()
    {
        health -= healthDecrement;

        // Invoke Update Health Event when the user misses targets

        UpdateHealth?.Invoke(health);
    }
}
