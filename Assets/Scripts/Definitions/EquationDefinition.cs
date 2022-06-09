using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Equation")]
    public class EquationDefinition : ScriptableObject
    {
        [InfoBox("Dont put X in, start with math sign. Allowed mathematical signs for equation are: +, -, *, /")]
        [SerializeField] public string mathEquation;

        




        // [SerializeField] public String equationToDisplay;
        //
        // [SerializeField] public MathCharacters firstMathCharacter;
        //
        // [HideIf("firstMathCharacter", MathCharacters.empty)]
        // [SerializeField] public float firstNumber;
        //
        // [HideIf("firstMathCharacter", MathCharacters.empty)]
        // [SerializeField] public MathCharacters secondMathCharacter;
        //
        // [HideIf("secondMathCharacter", MathCharacters.empty)]
        // [SerializeField] public float secondNumber;
    }
}
