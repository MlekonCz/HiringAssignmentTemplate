using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "LevelDefinition", order = 0)]
    public class LevelDefinition : ScriptableObject
    {
        [SerializeField] public EquationDefinition[] equationDefinitions;
        [SerializeField] public GameObject[] platformPrefabs;
        [SerializeField] public GameObject finalPlatform;
        
        [SerializeField] public bool isEndlessMode = false;
        [HideIf("isEndlessMode", true)]
        [SerializeField] public int platformsToSpawn = 10;
        
        
        
        
    }
}