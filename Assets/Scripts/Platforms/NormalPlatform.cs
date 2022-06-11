using System;
using System.Collections.Generic;
using Definitions;
using Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Platforms
{
    public class NormalPlatform : Platform
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private TMP_Text leftWall;
        [SerializeField] private TMP_Text rightWall;
        

        private List<EquationDefinition> equations = new List<EquationDefinition>();
        private EquationProvider _equationProvider;


        private void OnEnable()
        {
            normalWall.SetActive(true);
        }

        public override void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            base.Initialize(equationDefinitions, numberOfEnemies);
            
            equations.Clear();
            foreach (var equation in equationDefinitions)
            {
                equations.Add(equation);
            }
            AssignEquations();
        }
        private void AssignEquations()
        {
            leftWall.text ="x "  + equations[0].mathEquation; 
            rightWall.text ="x "  + equations[1].mathEquation;
        }

        
        public void TriggerMathGate(bool isLeft,GameObject player)
        {
            canvas.SetActive(false);
            if (isLeft)
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(equations[0].mathEquation);
            }
            else
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(equations[1].mathEquation);
            }
        }
    }
}
