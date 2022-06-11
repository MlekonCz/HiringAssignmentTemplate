using System;
using Definitions;
using Platforms;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private LevelDefinition levelDefinition;
        private LevelUiController _levelUiController;

        private void Initialize()
        {
            
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            } 
            
            Debug.Log("Initialized");
           _levelUiController = FindObjectOfType<LevelUiController>();
           
           FindObjectOfType<PlatformManager>().Initialize(levelDefinition);
           FindObjectOfType<EquationProvider>().Initialize(levelDefinition);
           FindObjectOfType<PlatformManager>().levelFinished += LevelFinished;
           FindObjectOfType<PlayerManager>().playerLost += LevelFinished;
        }

        
        
        
        
        
        

        
        
        
        
        
        
        
        private void LevelFinished(bool playerWon)
        {
            if (playerWon)
            {
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
            // FindObjectOfType<PlatformManager>().levelFinished -= LevelFinished;
           // FindObjectOfType<PlayerManager>().playerLost -= LevelFinished;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion
        
    }
}
