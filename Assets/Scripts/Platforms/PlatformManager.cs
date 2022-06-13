using System;
using System.Collections;
using Core;
using Definitions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private bool editPlatformsTransform = false;

        [ShowIf("editPlatformsTransform", true), SerializeField]
        private float _platformsPositionY = -0.5f;

        [ShowIf("editPlatformsTransform", true), SerializeField]
        private float _distanceBetweenPlatforms = 40f;

        private LevelDefinition _levelDefinition;

        private int _platformsToSpawn;
        private int _currentlySpawnedPlatforms;
        private int _platformsSpawned = 0;

        public Action<Platform> OnPlatformReleased;

        public delegate void CallBackType(bool playerWon);

        public event CallBackType LevelFinished;

        private IObjectPool<Platform> _platformPool;

        private void Awake()
        {
            if (PersistentObjects.Instance == null)
            {
                return;
            }

            Initialize();
        }

        private void Initialize()
        {
            _levelDefinition = PersistentObjects.Instance.GameManager.CurrentLevelDefinition;
            _platformPool = new ObjectPool<Platform>(CreatePlatform, OnGet, OnRelease, maxSize: 6);
            _currentlySpawnedPlatforms = GameObject.FindGameObjectsWithTag(TagManager.Platform).Length;
            _platformsSpawned = _currentlySpawnedPlatforms;
            _platformsToSpawn = _levelDefinition.PlatformsToSpawn;
            EquationProvider.Reset();
            OnPlatformReleased += ReleasePlatform;
        }

        private void OnDestroy()
        {
            OnPlatformReleased -= ReleasePlatform;
        }

        #region PlatformPool

        private Platform CreatePlatform()
        {
            Platform platform = (Platform) Instantiate(
                    _levelDefinition.PlatformPrefabs[Random.Range(0, _levelDefinition.PlatformPrefabs.Length)],
                    transform)
                .GetComponent<Platform>();
            if (platform == null)
            {
                Debug.LogError("Platform is not set");
                return null;
            }

            float zSpawnPosition = _platformsSpawned * _distanceBetweenPlatforms;
            platform.SetPosition(new Vector3(0, -1.5f, zSpawnPosition));
            platform.SetManager(this);
            return platform;
        }

        private void OnGet(Platform platform)
        {
            float zSpawnPosition = _platformsSpawned * _distanceBetweenPlatforms;
            platform.transform.position = new Vector3(0, _platformsPositionY, zSpawnPosition);
            platform.Initialize(
                EquationProvider.GetMathEquations(),
                EquationProvider.GetNumberOfEnemies(_levelDefinition.DifficultyOfNormalEnemies));

            _platformsSpawned++;
            _currentlySpawnedPlatforms++;
            platform.gameObject.SetActive(true);
        }

        private void ReleasePlatform(Platform platform)
        {
            StartCoroutine(DeactivatePlatform(platform));
        }

        private void OnRelease(Platform platform)
        {
            platform.gameObject.SetActive(false);
            _currentlySpawnedPlatforms--;
        }

        private IEnumerator DeactivatePlatform(Platform platform)
        {
            yield return new WaitForSeconds(3.5f);
            _platformPool.Release(platform);
        }

        #endregion


        private void Update()
        {
            SpawnPlatform();
        }

        private void SpawnPlatform()
        {
            if (_currentlySpawnedPlatforms >= 5)
            {
                return;
            }

            if (_platformsSpawned == 0)
            {
                InstantiateStartingPlatform();
            }
            else if (_platformsSpawned < _platformsToSpawn || _levelDefinition.IsEndlessMode)
            {
                _platformPool.Get();
            }
            else if (_platformsSpawned == _platformsToSpawn && !_levelDefinition.IsEndlessMode)
            {
                InstantiateFinalPlatform();
            }
        }

        #region spawningPlatforms

        private void InstantiateStartingPlatform()
        {
            GameObject platform = Instantiate(_levelDefinition.StartingPlatform, new Vector3(0, _platformsPositionY, 0),
                Quaternion.identity);
            _platformsSpawned++;
            Destroy(platform, 4f);
        }

        private void InstantiateFinalPlatform()
        {
            float zSpawnPosition = _platformsSpawned * _distanceBetweenPlatforms;
            GameObject platform = Instantiate(_levelDefinition.FinalPlatform,
                new Vector3(0, _platformsPositionY, zSpawnPosition), Quaternion.identity);
            platform.GetComponent<BossPlatform>().Initialize(EquationProvider.GetNumberOfEnemies
                (_levelDefinition.DifficultyOfBoss));
            _platformsSpawned++;
            SubscribeToFinalPlatform(platform);
        }

        #endregion

        private void SubscribeToFinalPlatform(GameObject finalPlatform)
        {
            finalPlatform.GetComponent<BossPlatform>().LevelFinished += CurrentLevelFinished;
        }

        private void CurrentLevelFinished()
        {
            LevelFinished?.Invoke(true);
        }
    }
}