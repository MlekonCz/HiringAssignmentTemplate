using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Equation")]
    public class EquationDefinition : ScriptableObject
    {
        [InfoBox("Dont put X in, start with math sign. Allowed mathematical signs for equation are: +, -, *, /")]
        [SerializeField] public string mathEquation;

    }
}
