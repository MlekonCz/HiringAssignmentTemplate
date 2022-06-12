using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string id = string.Empty;
        public string Id => id;

        
        [Button]
        private void GenerateId() => id = Guid.NewGuid().ToString();
        
        public object CaptureState()
        {
            var stateDictionary = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                stateDictionary[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return stateDictionary;
        }

        public void RestoreState(object state)
        {
            var stateDictionary = (Dictionary<string, object>) state;

            foreach (var saveable in GetComponents<ISaveable>())
            {
                var typeName = saveable.GetType().ToString();

                if (stateDictionary.TryGetValue(typeName, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}