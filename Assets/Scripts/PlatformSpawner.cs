using Definitions;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private LevelDefinition levelDefinition;

    private int _platformsToSpawn;
    private int _currentlySpawnedPlatforms;
    private int _platformsSpawned = 0;

    private void Start()
    {
        _currentlySpawnedPlatforms = GameObject.FindGameObjectsWithTag(TagManager.Platform).Length;
        _platformsSpawned = _currentlySpawnedPlatforms;
        _platformsToSpawn = levelDefinition.platformsToSpawn;
    }

    private void Update()
    {
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        if (_platformsSpawned < _platformsToSpawn)
        {
            float zSpawnPosition = _platformsSpawned * 40;
            
            Instantiate(levelDefinition.platformDefinitions[Random.Range(0, levelDefinition.platformDefinitions.Length)].platformPrefab,
                new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);
            
            _platformsSpawned++;
            _currentlySpawnedPlatforms++;
        }
        else if (_platformsSpawned == _platformsToSpawn)
        {
            float zSpawnPosition = _platformsSpawned * 40;
            Instantiate(levelDefinition.finalPlatform.platformPrefab, new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);
            _platformsSpawned++;
        }
    }
}
