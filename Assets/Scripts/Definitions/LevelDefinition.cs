using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "LevelDefinition", order = 0)]
    public class LevelDefinition : ScriptableObject
    {
        [SerializeField] public PlatformDefinition[] platformDefinitions;
        [SerializeField] public EquationDefinition[] equationDefinitions;
        [SerializeField] public PlatformDefinition finalPlatform;
        
        [SerializeField] public bool isInfiniteMode = false;
        [HideIf("isInfiniteMode", true)]
        [SerializeField] public int platformsToSpawn = 10;
        
        
        
        
    }
}