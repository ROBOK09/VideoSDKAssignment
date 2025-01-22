using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    // Public static coroutine so that Enemy dependency is not required

    public static event Action hitTarget;
    public static event Action missTarget;

    [SerializeField]
    private SpriteRenderer miniMapIcon;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            // Event fired if object was hit by the bullet

            hitTarget?.Invoke();
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetSpawnedEnemyAttributes(int enemyDespawnTime, Color color)
    {
        // Set new material according to the color passed

        Renderer objRenderer = transform.GetComponent<Renderer>();
        Material newMaterial = new Material(objRenderer.sharedMaterial);
        newMaterial.color = color;
        objRenderer.material = newMaterial;

        // Set new material according to the color passed

        miniMapIcon.color = color;

        // Start Despawn Coroutine as soon as the object attributes are set

        StartCoroutine(DespawnEnemyGameObjectCoR(enemyDespawnTime));
    }

    // Despawn Coroutine

    public IEnumerator DespawnEnemyGameObjectCoR(int despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);

        // Event fired if object was missed by user

        missTarget?.Invoke();
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
