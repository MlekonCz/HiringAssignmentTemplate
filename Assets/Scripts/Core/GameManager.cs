using System;
using Definitions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private int _accessibleLevel = 1;


        [InfoBox("Endless mode level must be last in array")] [SerializeField]
        private LevelDefinition[] _levelDefinitions;

        public LevelDefinition CurrentLevelDefinition => _levelDefinitions[SceneManager.GetActiveScene().buildIndex - 1];

        public Action<bool> OnLevelFinished;
        
        
        public int AccessibleLevel => _accessibleLevel;

        private void Awake()
        {
            OnLevelFinished += LevelFinished;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                PersistentObjects.Instance.SavingSystem.Load();
                return;
            }
        }
        
        private void LevelFinished(bool playerWon)
        {
            if (playerWon)
            {
                _accessibleLevel++;
                PersistentObjects.Instance.SavingSystem.Save();
            }
        }

        #region Subscriptions

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneUnLoaded(Scene scene)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneUnLoaded;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        public object CaptureState()
        {
            return new SaveData
            {
                accessibleLevel = _accessibleLevel,
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData) state;
            _accessibleLevel = saveData.accessibleLevel;
        }


        [Serializable]
        private struct SaveData
        {
            public int accessibleLevel;
        }
    }
}