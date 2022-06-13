using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Equation")]
    public class EquationDefinition : ScriptableObject
    {
        [FormerlySerializedAs("mathEquation")]
        [InfoBox("Dont put X in, start with math sign. Allowed mathematical signs for equation are: +, -, *, /")]
        [SerializeField] public string MathEquation;

    }
}
