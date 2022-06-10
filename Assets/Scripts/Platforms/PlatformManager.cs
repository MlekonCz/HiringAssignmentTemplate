using Definitions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private LevelDefinition levelDefinition;
        private EquationProvider _equationProvider;

        private int _platformsToSpawn;
        private int _currentlySpawnedPlatforms;
        private int _platformsSpawned = 0;

        private void Start()
        {
            _equationProvider = FindObjectOfType<EquationProvider>();
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
            //if (_currentlySpawnedPlatforms >= 4){return;}
            
            if (_platformsSpawned < _platformsToSpawn)
            {
                InstantiatePlatform();
            }
            else if (_platformsSpawned == _platformsToSpawn)
            {
                InstantiateFinalPlatform();
            }
        }
        
        private void InstantiatePlatform()
        {
            float zSpawnPosition = _platformsSpawned * 40;

            GameObject platform = Instantiate(
                levelDefinition.platformPrefabs[Random.Range(0, levelDefinition.platformPrefabs.Length)],
                new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);

            platform.GetComponent<Platform>().Initialize(_equationProvider.GetMathEquations(),
                _equationProvider.GetNumberOfEnemies(levelDefinition.difficultyOfNormalEnemies));

            _platformsSpawned++;
            _currentlySpawnedPlatforms++;
        }
        private void InstantiateFinalPlatform()
        {
            float zSpawnPosition = _platformsSpawned * 40;
            Instantiate(levelDefinition.finalPlatform, new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);
            _platformsSpawned++;
        }

    }
}
