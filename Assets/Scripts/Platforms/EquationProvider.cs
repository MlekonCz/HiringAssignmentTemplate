using System;
using System.Collections.Generic;
using System.Data;
using Core;
using Definitions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    
    public static class EquationProvider
    {
        private static float _highestPossibleNumber = 1;
        private static List<EquationDefinition> s_equiations = new List<EquationDefinition>();

        public static void Reset()
        {
            _highestPossibleNumber = 1;
        }
        
        public static List<EquationDefinition> GetMathEquations()
        { 
            s_equiations = new List<EquationDefinition>();
            int index1 = 0;
            int index2= 0;
        
            var equationDefinition = PersistentObjects.Instance.GameManager.CurrentLevelDefinition.EquationDefinitions;

            while (index1 == index2)
            {
                index1 = Random.Range(0, equationDefinition.Length);
                index2 = Random.Range(0, equationDefinition.Length);
            }

            s_equiations.Add(equationDefinition[index1]); 
            s_equiations.Add(equationDefinition[index2]); 
            
            SimulateBestScore();
            
            return s_equiations;
        }

        private static void SimulateBestScore()
        {
            var equationDefinitions = PersistentObjects.Instance.GameManager.CurrentLevelDefinition.EquationDefinitions;
            
            string number1 = _highestPossibleNumber.ToString() + s_equiations[0].MathEquation;
            string number2 = _highestPossibleNumber.ToString() + s_equiations[1].MathEquation;
        
            double firstNumber = Convert.ToDouble(new DataTable().Compute
                (number1,null));
            double secondNumber = Convert.ToDouble(new DataTable().Compute
                (number2,null));
        
            if ((float)firstNumber > (float)secondNumber)
            {
                _highestPossibleNumber = (float)firstNumber;
            }
            else
            {
                _highestPossibleNumber = (float)secondNumber;
            }
            _highestPossibleNumber = Mathf.CeilToInt(_highestPossibleNumber);
        }
        public static int GetNumberOfEnemies(float percentage)
        {
            int enemies = 0;

            enemies = Mathf.FloorToInt(_highestPossibleNumber * percentage);
            enemies -= 1;
            if (enemies <0)
            {
                enemies = 0;
            }
            _highestPossibleNumber -= enemies;
            
            return enemies;
        }
    }
}
