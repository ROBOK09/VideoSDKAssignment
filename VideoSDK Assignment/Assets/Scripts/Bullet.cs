using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int bulletDespawnTime = 5;

    private void Awake()
    {
        // Start despawn Timer as soon as the object is loaded

        DespawnBulletGameObjectCoR(bulletDespawnTime);
    }

    private void Start()
    {
        // Subscribing to various events fired on Game State Changes (Pub - Sub Pattern)

        UIManager.Instance.GameRestarted += DestroyBulletGameObject;
        UIManager.Instance.GameOver += DestroyBulletGameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Gun"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        // Desubscribing to various events fired on Game State Changes to prevent memory leaks (Pub - Sub Pattern)

        UIManager.Instance.GameRestarted -= DestroyBulletGameObject;
        UIManager.Instance.GameOver -= DestroyBulletGameObject;
    }

    // Despawn Coroutine

    public IEnumerator DespawnBulletGameObjectCoR(int despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        DestroyBulletGameObject();
    }

    private void DestroyBulletGameObject()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
