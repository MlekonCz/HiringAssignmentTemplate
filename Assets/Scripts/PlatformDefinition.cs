using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformDefinition")]
public class PlatformDefinition : ScriptableObject
{ 
    [SerializeField] public String equation = "x + 5"; 
    [InfoBox("Size of the new model will be changed to (20f,1f,40f)")]
    [SerializeField] public GameObject platformModelOverride;


}
