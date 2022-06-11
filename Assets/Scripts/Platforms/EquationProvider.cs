using System;
using System.Collections.Generic;
using System.Data;
using Definitions;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class EquationProvider : MonoBehaviour
    {
        private LevelDefinition levelDefinition;
        private List<EquationDefinition> equation = new List<EquationDefinition>();
        private List<EquationDefinition> givenEquations = new List<EquationDefinition>();

        [SerializeField] private float highestPossibleNumber;
        private int currentNumber;
    
        private int index1;
        private int index2;

        private void Start()
        {
            highestPossibleNumber = FindObjectOfType<PlayerManager>().currentNumber;
            currentNumber = Mathf.FloorToInt(highestPossibleNumber);
        }
        public void Initialize(LevelDefinition currentLevel)
        {
            levelDefinition = currentLevel;
        }

        public List<EquationDefinition> GetMathEquations()
        {
            equation.Clear();
            index1 = 0;
            index2 = 0;
        
            var equationDefinition = levelDefinition.equationDefinitions;

            while (index1 == index2)
            {
                index1 = Random.Range(0, equationDefinition.Length);
                index2 = Random.Range(0, equationDefinition.Length);
            }

            equation.Add(equationDefinition[index1]); 
            equation.Add(equationDefinition[index2]); 
        
            SimulateBestScore();
        
            return equation;
        }

        private void SimulateBestScore()
        {
            string number1 = highestPossibleNumber.ToString() + equation[0].mathEquation;
            string number2 = highestPossibleNumber.ToString() + equation[1].mathEquation;
        
            double firstNumber = Convert.ToDouble(new DataTable().Compute
                (number1,null));
            double secondNumber = Convert.ToDouble(new DataTable().Compute
                (number2,null));
        
            if ((float)firstNumber > (float)secondNumber)
            {
                highestPossibleNumber = (float)firstNumber;
            }
            else
            {
                highestPossibleNumber = (float)secondNumber;
            }

            highestPossibleNumber = Mathf.CeilToInt(highestPossibleNumber);
        }

  

        public int GetNumberOfEnemies(float percentage)
        {
            int enemies = 0;

            enemies = Mathf.FloorToInt(highestPossibleNumber * percentage);
            enemies -= 1;
        
            if (enemies <= 0)
            {
                enemies = 1;
            }
            highestPossibleNumber -= enemies;
            
            return enemies;
        }
    

// String numberTest = "5 + 3";
        // double result = Convert.ToDouble(new DataTable().Compute(numberTest, null));


    }
}
