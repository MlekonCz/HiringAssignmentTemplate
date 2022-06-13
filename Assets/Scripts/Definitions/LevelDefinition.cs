using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Definitions
{
    [CreateAssetMenu(fileName = "LevelDefinition", order = 0)]
    public class LevelDefinition : ScriptableObject
    {
        [FormerlySerializedAs("equationDefinitions")]
        [BoxGroup("Platforms")]
        [SerializeField] public EquationDefinition[] EquationDefinitions;
        [FormerlySerializedAs("platformPrefabs")]
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject[] PlatformPrefabs;
        [FormerlySerializedAs("finalPlatform")]
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject FinalPlatform;
        [FormerlySerializedAs("startingPlatform")]
        [BoxGroup("Platforms")]
        [SerializeField] public GameObject StartingPlatform;

        [FormerlySerializedAs("isEndlessMode")]
        [BoxGroup("Platform number")]
        [SerializeField] public bool IsEndlessMode = false;
        [FormerlySerializedAs("platformsToSpawn")]
        [BoxGroup("Platform number")]
        [HideIf("IsEndlessMode", true)]
        [SerializeField] public int PlatformsToSpawn = 10;

        [FormerlySerializedAs("difficultyOfNormalEnemies")]
        [BoxGroup("Difficulty")]
        [InfoBox("Percentile difficulty of how much of an error player can make 1f means that once player makes one bad choice he wont survive")]
        [Range(0.2f, 1f)] [SerializeField] public float DifficultyOfNormalEnemies;
        [FormerlySerializedAs("difficultyOfBoss")]
        [BoxGroup("Difficulty")]
        [Range(0.5f, 1f)] [SerializeField] public float DifficultyOfBoss;

        [FormerlySerializedAs("playerSpeed")]
        [BoxGroup("Player")]
        [SerializeField] public float PlayerSpeed;
    }
}