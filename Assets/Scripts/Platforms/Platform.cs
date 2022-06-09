using System;
using System.Collections.Generic;
using System.Data;
using Definitions;
using TMPro;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;

        private EquationDefinition[] equations = new EquationDefinition[2];
        private EquationProvider equationProvider;

        private void Start()
        {
            // String numberTest = "5 + 3";
            // double result = Convert.ToDouble(new DataTable().Compute(numberTest, null));
            
            equationProvider = FindObjectOfType<EquationProvider>();
            equations = equationProvider.GetMathEquations();
            Debug.Log(equations.Length);

            AssignEquations();
        }

        private void AssignEquations()
        {
            leftWall.GetComponent<TMP_Text>().text ="x "  + equations[0].mathEquation; 
            rightWall.GetComponent<TMP_Text>().text ="x "  + equations[1].mathEquation;
        }

        // private String enumToString(bool isLeft)
        // {
        //     if (isLeft)
        //     {
        //         string usedSign = null;
        //         
        //         switch (equations[0].firstMathCharacter.ToString())
        //         {
        //             case "empty":
        //                 Debug.LogError("Didnt select math sign for EquationDefinition");
        //                 break;
        //             case "plus":
        //                 usedSign = "+ ";
        //                 break;
        //             case "minus":
        //                 usedSign = "- ";
        //                 break;
        //             case "multiple":
        //                 usedSign = "* ";
        //                 break;
        //             case "division":
        //                 usedSign = "/ ";
        //                 break;
        //             case "equal":
        //                 usedSign = "= ";
        //                 break;
        //         }
        //
        //         return usedSign;
        //     }
        //     else
        //     {
        //         string usedSign = null;
        //         switch (equations[1].firstMathCharacter.ToString())
        //         {
        //             case "empty":
        //                 Debug.LogError("Didnt select math sign for EquationDefinition");
        //                 break;
        //             case "plus":
        //                 usedSign = "+ ";
        //                 break;
        //             case "minus":
        //                 usedSign = "- ";
        //                 break;
        //             case "multiple":
        //                 usedSign = "* ";
        //                 break;
        //             case "division":
        //                 usedSign = "/ ";
        //                 break;
        //             case "equal":
        //                 usedSign = "= ";
        //                 break;
        //         }
        //         return usedSign;
        //     }
        //     
        // }
        private void Update()
        {
        
        }

    }
}
