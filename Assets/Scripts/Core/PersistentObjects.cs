using UnityEngine;

namespace Core
{
    public class PersistentObjects : MonoBehaviour
    {

        public static PersistentObjects Instance;

        [SerializeField] private GameManager _gameManagerPrefab;
        [SerializeField] private SavingSystem _savingSystemPrefab;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;


        private GameManager _gameManagerInstance;
        private SavingSystem _savingSystemInstance;
        private SceneLoader _sceneLoaderInstance;

        public GameManager GameManager => _gameManagerInstance;
        public SavingSystem SavingSystem => _savingSystemInstance;
        public SceneLoader SceneLoader => _sceneLoaderInstance;

        private static bool _hasSpawned = false;

        private void Awake()
        {
            if (Instance == null)
            {
                if (_hasSpawned){return;}
                SpawnPersistentObject();
                _hasSpawned = true;
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            else
            {
                Destroy(gameObject);
            }
           
        }

        private void SpawnPersistentObject()
        {
           _gameManagerInstance = Instantiate(_gameManagerPrefab,transform);
           _savingSystemInstance = Instantiate(_savingSystemPrefab,transform);
           _sceneLoaderInstance = Instantiate(_sceneLoaderPrefab,transform);
        }
    }
}