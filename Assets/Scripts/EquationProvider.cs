using System;
using System.Collections.Generic;
using System.Data;
using Definitions;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class EquationProvider : MonoBehaviour
{
    [SerializeField] private LevelDefinition levelDefinition;
    private List<EquationDefinition> equation = new List<EquationDefinition>();
    private List<EquationDefinition> givenEquations = new List<EquationDefinition>();

    private float highestPossibleNumber;
    private int currentNumber;
    
    private int index1;
    private int index2;

    private void Start()
    {
        highestPossibleNumber = FindObjectOfType<PlayerManager>().currentNumber;
        currentNumber = Mathf.FloorToInt(highestPossibleNumber);
    }

    public List<EquationDefinition> GetMathEquations()
    {
        equation.Clear();
        
        var equationDefinition = levelDefinition.equationDefinitions;
        Debug.Log(equationDefinition[2]);


        index1 = Random.Range(0, equationDefinition.Length);
        index2 = Random.Range(0, equationDefinition.Length);
        while (index1 == index2)
        {
            index1 = Random.Range(0, equationDefinition.Length);
            index2 = Random.Range(0, equationDefinition.Length);
        }

        equation.Add(equationDefinition[index1]); 
        equation.Add(equationDefinition[index2]); 
        
        GetHigherNumber();
        
        return equation;
    }

    private void GetHigherNumber()
    {
        double firstNumber = Convert.ToDouble(new DataTable().Compute
            (highestPossibleNumber.ToString() + equation[0].mathEquation,null));
        double secondNumber = Convert.ToDouble(new DataTable().Compute
            (highestPossibleNumber.ToString() + equation[1].mathEquation,null));
        if ((float)firstNumber > (float)secondNumber)
        {
            highestPossibleNumber = (float)firstNumber;
        }
        else
        {
            highestPossibleNumber = (float)secondNumber;
        }
    }

    public void SelectedMathEquation(String mathEquation)
    {
        Debug.Log(mathEquation);
        double result = Convert.ToDouble(new DataTable().Compute
            (currentNumber.ToString() + mathEquation,null));

        currentNumber = (int)result;
        
        
        Debug.Log(currentNumber);
    }
    
    

// String numberTest = "5 + 3";
    // double result = Convert.ToDouble(new DataTable().Compute(numberTest, null));


}
