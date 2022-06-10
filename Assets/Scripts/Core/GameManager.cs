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
        
        [SerializeField] private GameObject winCanvas;
        [SerializeField] private GameObject loseCanvas;
        
        
        [SerializeField] private LevelDefinition levelDefinition;

        private void Awake()
        {
            
        }

        private void Initialize()
        {
            
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
           Debug.Log("Initialized");
            FindObjectOfType<PlatformManager>().Initialize(levelDefinition);
            FindObjectOfType<EquationProvider>().Initialize(levelDefinition);
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            
            FindObjectOfType<PlatformManager>().levelFinished += LevelFinished;
            FindObjectOfType<PlayerManager>().playerLost += LevelFinished;
        }

        
        
        
        
        
        

        
        
        
        
        
        
        
        private void LevelFinished(bool playerWon)
        {
            Debug.Log("Level finished");
            if (playerWon)
            {
                winCanvas.SetActive(true);
                Debug.Log("You Won!!!");
            }
            else
            {
                loseCanvas.SetActive(true);
                Debug.Log("You lost. Better luck next time.");
            }
        }

        #region Subscriptions
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }
        
        void OnSceneUnLoaded(Scene scene)
        {
            FindObjectOfType<PlatformManager>().levelFinished -= LevelFinished;
            FindObjectOfType<PlayerManager>().playerLost -= LevelFinished;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion
        
    }
}
