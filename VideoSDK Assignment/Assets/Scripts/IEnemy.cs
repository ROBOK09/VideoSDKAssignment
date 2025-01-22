using UnityEngine;

// IEnemy interface that can be inherited by the classes and implemented according to themselves to get themselves modified by the Factory/ Factory Constructor classes (factory Pattern)

public interface IEnemy
{
    // abstract method that can be used by other objects that implement the IEnemy Interface

    void SetSpawnedEnemyAttributes(int enemyDespawnTime, Color color);
}
