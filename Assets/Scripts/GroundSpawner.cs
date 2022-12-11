using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Ground ground;
    [SerializeField] private Ground firstGround;

    private readonly List<Ground> _spawnedGrounds = new();

    private void Start()
    {
        _spawnedGrounds.Add(firstGround);
    }

    private void Update()
    {
        if (playerTransform.position.x > _spawnedGrounds[^1].end.position.x - 6)
        {
            SpawnGround();
        }
    }

    private void SpawnGround()
    {
        Ground newGround = Instantiate(ground);
        newGround.transform.position =
            _spawnedGrounds[^1].end.position - newGround.start.position;
        _spawnedGrounds.Add(newGround);
        if (_spawnedGrounds.Count >= 3)
        {
            Destroy(_spawnedGrounds[0].gameObject);
            _spawnedGrounds.RemoveAt(0);
        }
    }
}