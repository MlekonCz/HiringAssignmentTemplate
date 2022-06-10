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
        private EquationProvider equationProvider;

        private void Start()
        {
            equations.Clear();
            // String numberTest = "5 + 3";
            // double result = Convert.ToDouble(new DataTable().Compute(numberTest, null));
            
            equationProvider = FindObjectOfType<EquationProvider>();
            foreach (var equation in equationProvider.GetMathEquations())
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
                equationProvider.SelectedMathEquation(equations[0].mathEquation);
            }
            else
            {
                equationProvider.SelectedMathEquation(equations[1].mathEquation);
            }
        }
        
        private void Update()
        {
        
        }

    }
}
