using UnityEngine;

namespace Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab = null;

        private static bool _hasSpawned = false;

        private void Awake()
        {
            if (_hasSpawned){return;}
            SpawnPersistentObject();
            _hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}