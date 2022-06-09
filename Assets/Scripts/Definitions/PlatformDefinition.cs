using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "PlatformDefinition")]
    public class PlatformDefinition : ScriptableObject
    { 
        [SerializeField] public String equation = "x + 5"; 
        [InfoBox("Platform needs to be of size: (20f,1f,40f)")]
        [SerializeField] public GameObject platformPrefab;


    }
}
