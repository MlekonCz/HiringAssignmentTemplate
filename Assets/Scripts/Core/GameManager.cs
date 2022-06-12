using System;
using Definitions;
using Platforms;
using Player;
using Scenes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour, ISaveable
    {

        [InfoBox("Endless mode level must be last in array")]
        [SerializeField] private LevelDefinition[] levelDefinition;
        
        [SerializeField] private int accessibleLevel = 1;
        
        private LevelUiController _levelUiController;
        private SavingSystem _savingSystem;
        
        
        private int _currentScene;

        
        private void Awake()
        {
            _savingSystem = FindObjectOfType<SavingSystem>();
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _currentScene = SceneManager.GetActiveScene().buildIndex;
            
            if (_currentScene == 0)
            {
                _savingSystem.Load();
                FindObjectOfType<MainMenuUiController>().SetAccessibleLevel(accessibleLevel);
                return;
            }
            _levelUiController = FindObjectOfType<LevelUiController>();
           
            InitializeCurrentLevel();
        }

        private void InitializeCurrentLevel()
        {
            FindObjectOfType<PlayerMover>().SetPlayerSpeed(levelDefinition[_currentScene - 1].playerSpeed);
            FindObjectOfType<PlatformManager>().Initialize(levelDefinition[_currentScene - 1]);
            FindObjectOfType<EquationProvider>().Initialize(levelDefinition[_currentScene - 1]);
            FindObjectOfType<PlatformManager>().levelFinished += LevelFinished;
            FindObjectOfType<PlayerManager>().playerLost += LevelFinished;
        }


        private void LevelFinished(bool playerWon)
        {
            if (playerWon)
            {
                accessibleLevel++;
                _savingSystem.Save();
                _levelUiController.ShowWonMenu();
            }
            else
            {
               _levelUiController.ShowLostMenu();
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
                accessibleLevel = accessibleLevel,
            };
        }
        public void RestoreState(object state)
        {
            var saveData = (SaveData) state;
            accessibleLevel = saveData.accessibleLevel;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public int accessibleLevel;
        }

       
    }
}
