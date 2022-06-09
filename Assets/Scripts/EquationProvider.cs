using System;
using System.Collections.Generic;
using Definitions;
using UnityEngine;
using Random = UnityEngine.Random;

public class EquationProvider : MonoBehaviour
{
    [SerializeField] private LevelDefinition levelDefinition;
    private EquationDefinition[] equation = new EquationDefinition[2];

    private int index1;
    private int index2;

    public EquationDefinition[] GetMathEquations()
    {
        Array.Clear(equation,0,equation.Length);
        
        var equationDefinition = levelDefinition.equationDefinitions;
        Debug.Log(equationDefinition[2]);


        index1 = Random.Range(0, equationDefinition.Length);
        index2 = Random.Range(0, equationDefinition.Length);
        while (index1 == index2)
        {
            index1 = Random.Range(0, equationDefinition.Length);
            index2 = Random.Range(0, equationDefinition.Length);
        }

        equation[0] = equationDefinition[index1]; 
        equation[1] = equationDefinition[index2]; 
        return equation;
    }




}
