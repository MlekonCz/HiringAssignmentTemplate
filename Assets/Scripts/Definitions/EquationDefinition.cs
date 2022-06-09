using Sirenix.OdinInspector;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Equation")]
    public class EquationDefinition : ScriptableObject
    {
        [SerializeField] public MathCharacters firstMathCharacter;
        
        [InfoBox("X and first math character cant be written in. And allowed mathematical signs for equation are: +, -, *, /")]
        [SerializeField] private string mathEquation;

        




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
