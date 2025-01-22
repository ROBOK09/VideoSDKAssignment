using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip hitSound;

    private AudioSource audioSource;

    private static AudioSourceManager instance;

    // AudioSourceManager (Singleton Pattern)

    public static AudioSourceManager Instance 
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

        audioSource = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Subscribing to event fired on target hit (Pub - Sub Pattern)

        Enemy.hitTarget += PlayHitSound;
    }

    private void OnDisable()
    {
        // Desubscribing to event fired on target hit (Pub - Sub Pattern)

        Enemy.hitTarget -= PlayHitSound;
    }

    private void PlayHitSound()
    {
        // Playing OneShotAudio

        audioSource.PlayOneShot(hitSound);
    }
}
