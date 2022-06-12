using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "LevelDefinition", order = 0)]
    public class LevelDefinition : ScriptableObject
    {
        [BoxGroup("Platforms")]
        [SerializeField] public EquationDefinition[] equationDefinitions;
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject[] platformPrefabs;
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject finalPlatform;
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject startingPlatform;

        [BoxGroup("Platform number")]
        [SerializeField] public bool isEndlessMode = false;
        [BoxGroup("Platform number")]
        [HideIf("isEndlessMode", true)]
        [SerializeField] public int platformsToSpawn = 10;

        [BoxGroup("Difficulty")]
        [InfoBox("Percentile difficulty of how much of an error player can make 1f means that once player makes one bad choice he wont survive")]
        [Range(0.2f, 1f)] [SerializeField] public float difficultyOfNormalEnemies;
        [BoxGroup("Difficulty")]
        [Range(0.5f, 1f)] [SerializeField] public float difficultyOfBoss;

        [BoxGroup("Player")]
        [SerializeField] public float playerSpeed;
    }
}