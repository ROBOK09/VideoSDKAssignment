using UnityEngine;

public class EnemyFactory
{
    // Constant values for attribute assignment based on enemy type

    private const int GREEN_GHOST_LIFETIME = 7;
    private readonly Color GREEN_GHOST_COLOR = Color.green;
    private const int BLUE_SHADE_LIFETIME = 5;
    private readonly Color BLUE_SHADE_COLOR = Color.blue;
    private const int RED_PHANTOM_LIFETIME = 3;
    private readonly Color RED_PHANTOM_COLOR = Color.red;

    // Function to set object attributes (Factory Design Pattern)

    public void SpawnNewEnemy(GameObject enemyGameObject, EnemyType enemyType)
    {
        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        switch (enemyType)
        {
            case EnemyType.GREEN_GHOST:
                enemy.SetSpawnedEnemyAttributes(GREEN_GHOST_LIFETIME, GREEN_GHOST_COLOR);
                break;

            case EnemyType.BLUE_SHADE:
                enemy.SetSpawnedEnemyAttributes(BLUE_SHADE_LIFETIME, BLUE_SHADE_COLOR);
                break;

            case EnemyType.RED_PHANTOM:
                enemy.SetSpawnedEnemyAttributes(RED_PHANTOM_LIFETIME, RED_PHANTOM_COLOR);
                break;

            default:
                Debug.LogError("Invalid enemy type!");
                break;
        }
    }
}
