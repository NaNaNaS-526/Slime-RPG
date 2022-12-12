using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform playerTransform;

    private void OnEnable()
    {
        EventBus.OnDead += SpawnEnemy;
    }

    private void OnDisable()
    {
        EventBus.OnDead -= SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        var playerPosition = playerTransform.position;
        Instantiate(enemy, new Vector3(playerPosition.x + Random.Range(12, 20), 0.75f, playerPosition.z),
            Quaternion.identity);
    }
}