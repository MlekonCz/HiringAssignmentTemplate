using System;
using System.Data;
using Definitions;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;

        private EquationDefinition equationDefinition;

        private void Start()
        {
            AssignEquations();
            String numberTest = "5 + 3";
            double result = Convert.ToDouble(new DataTable().Compute(numberTest, null));
            Debug.Log(result);

        }

        private void AssignEquations()
        {
            //      leftWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
            //     rightWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
        }

        private void Update()
        {
        
        }

    }
}
