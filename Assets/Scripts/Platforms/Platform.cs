using System.Collections.Generic;
using Definitions;
using TMPro;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;

        [SerializeField] private List<EquationDefinition> equations;
        
        private EquationProvider _equationProvider;

        [SerializeField] private int enemies;

        private void Start()
        {
            
        }

        public void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            enemies = numberOfEnemies;
            equations.Clear();
            foreach (var equation in equationDefinitions)
            {
                equations.Add(equation);
            }
            AssignEquations();
        }

        private void AssignEquations()
        {
            
            leftWall.GetComponent<TMP_Text>().text ="x "  + equations[0].mathEquation; 
            rightWall.GetComponent<TMP_Text>().text ="x "  + equations[1].mathEquation;
        }

        public void TriggerMathGate(bool isLeft)
        {
            canvas.SetActive(false);
            if (isLeft)
            {
                _equationProvider.ChosenMathEquation(equations[0].mathEquation);
            }
            else
            {
                _equationProvider.ChosenMathEquation(equations[1].mathEquation);
            }
        }
    }
}
