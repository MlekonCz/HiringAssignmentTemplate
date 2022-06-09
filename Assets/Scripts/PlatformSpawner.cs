using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject finalPlatform;

    [SerializeField] private int platformsToSpawn = 10;

    private int currentlySpawnedPlatforms;
    
    private int _platformsSpawned = 0;

    private void Start()
    {
        currentlySpawnedPlatforms = GameObject.FindGameObjectsWithTag(TagManager.Platform).Length;
        Debug.Log(currentlySpawnedPlatforms);
        _platformsSpawned = currentlySpawnedPlatforms;
    }

    private void Update()
    {
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        if (_platformsSpawned < platformsToSpawn)
        {
            float zSpawnPosition = _platformsSpawned * 40;
            Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)], new Vector3(0, -1.5f, zSpawnPosition),
                Quaternion.identity);
            _platformsSpawned++;
            currentlySpawnedPlatforms++;
        }
        else if (_platformsSpawned == platformsToSpawn)
        {
            float zSpawnPosition = _platformsSpawned * 40;
            Instantiate(finalPlatform, new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);
            _platformsSpawned++;
        }
    }
}
