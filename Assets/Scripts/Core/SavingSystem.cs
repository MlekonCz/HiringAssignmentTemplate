using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public class SavingSystem : MonoBehaviour
    {
        private string _SavePath => $"{Application.persistentDataPath}/save.txt";

        [Button]
        public  void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }
        [Button]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(_SavePath))
            {
                return new Dictionary<string, object>();
            }
            else
            {
                using FileStream stream = File.Open(_SavePath, FileMode.Open);
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>) formatter.Deserialize(stream);
            }
           
        }

        private void SaveFile(object state)
        {
            using var stream = File.Open(_SavePath, FileMode.Create);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}