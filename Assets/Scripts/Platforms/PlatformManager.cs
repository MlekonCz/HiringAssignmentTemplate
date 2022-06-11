using System;
using System.Collections;
using Definitions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private bool editPlatformsTransform = false;
        
        [ShowIf("editPlatformsTransform", true)]
        [SerializeField] private float platformsPositionY = -0.5f;
        [ShowIf("editPlatformsTransform", true)]
        [SerializeField] private float distanceBetweenPlatforms = 40f;
        
        private LevelDefinition levelDefinition;
        private EquationProvider _equationProvider;

        private int _platformsToSpawn;
        private int _currentlySpawnedPlatforms;
        private int _platformsSpawned = 0;

        public delegate void CallBackType(bool playerWon);
        public event CallBackType levelFinished;

        private IObjectPool<GameObject> _platformPool;

        private void Awake()
        {
            _platformPool = new ObjectPool<GameObject>(CreatePlatform, OnGet, OnRelease, maxSize: 6);
        }
        private GameObject CreatePlatform()
        {
            float zSpawnPosition = _platformsSpawned * distanceBetweenPlatforms;
            
            GameObject platform = Instantiate(
                levelDefinition.platformPrefabs[Random.Range(0, levelDefinition.platformPrefabs.Length)],
                new Vector3(0, -1.5f, zSpawnPosition), Quaternion.identity);
            platform.GetComponent<NormalPlatform>().platformCleared += OnRelease;
            platform.GetComponent<NormalPlatform>().SetPool(_platformPool);
            
            return platform;
        }
        private void OnGet(GameObject platform)
        {
            platform.SetActive(true);

            
            float zSpawnPosition = _platformsSpawned * distanceBetweenPlatforms;
            platform.transform.position = new Vector3(0, platformsPositionY, zSpawnPosition);
            
            platform.GetComponent<NormalPlatform>().Initialize(_equationProvider.GetMathEquations(),
                _equationProvider.GetNumberOfEnemies(levelDefinition.difficultyOfNormalEnemies));
            
            _platformsSpawned++;
            _currentlySpawnedPlatforms++;
        }
        private void OnRelease(GameObject platform)
        {
            StartCoroutine(DeactivatePlatform(platform));
        }

        IEnumerator DeactivatePlatform(GameObject platform)
        {
            yield return new WaitForSeconds(2.5f);
            platform.SetActive(false);
            _currentlySpawnedPlatforms--;
        }
        private void Start()
        {
            _equationProvider = FindObjectOfType<EquationProvider>();
            _currentlySpawnedPlatforms = GameObject.FindGameObjectsWithTag(TagManager.Platform).Length;
            _platformsSpawned = _currentlySpawnedPlatforms;
            _platformsToSpawn = levelDefinition.platformsToSpawn;
        }

        public void Initialize(LevelDefinition currentLevel)
        {
            levelDefinition = currentLevel;
        }

        private void Update()
        {
            SpawnPlatform();
        }

        private void SpawnPlatform()
        {
            if (_currentlySpawnedPlatforms >= 5){return;}
            
            if (_platformsSpawned == 0)
            {
                InstantiateStartingPlatform();
            }
            else if (_platformsSpawned < _platformsToSpawn)
            {
                InstantiatePlatform();
            }
            else if (_platformsSpawned == _platformsToSpawn)
            {
                InstantiateFinalPlatform();
            }
        }
        #region spawningPlatforms
        private void InstantiateStartingPlatform()
        {
            GameObject platform = Instantiate(levelDefinition.startingPlatform, new Vector3(0, platformsPositionY, 0), Quaternion.identity);
            _platformsSpawned++;
            Destroy(platform, 4f);
        }
        private void InstantiatePlatform()
        {
            _platformPool.Get();
        }
        private void InstantiateFinalPlatform()
        {
            float zSpawnPosition = _platformsSpawned * distanceBetweenPlatforms;
            GameObject platform = Instantiate(levelDefinition.finalPlatform, new Vector3(0, platformsPositionY, zSpawnPosition), Quaternion.identity);
            platform.GetComponent<BossPlatform>().Initialize(_equationProvider.GetNumberOfEnemies
                (levelDefinition.difficultyOfBoss));
            _platformsSpawned++;
            SubscribeToFinalPlatform(platform);
        }
        #endregion
        private void SubscribeToFinalPlatform(GameObject finalPlatform)
        {
            finalPlatform.GetComponent<BossPlatform>().levelFinished += CurrentLevelFinished;
        }
        private void CurrentLevelFinished()
        {
            levelFinished?.Invoke(true);
        }
    }
}
