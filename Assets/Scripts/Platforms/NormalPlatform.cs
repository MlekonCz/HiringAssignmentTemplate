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
        

        private List<EquationDefinition> _equations = new List<EquationDefinition>();
        private EquationProvider _equationProvider;
        
        public delegate void CallBackType(GameObject platform);
        public event CallBackType platformCleared;


        private void OnEnable()
        {
            normalWall.SetActive(true);
        }

        public override void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            base.Initialize(equationDefinitions, numberOfEnemies);
            
            _equations.Clear();
            foreach (var equation in equationDefinitions)
            {
                _equations.Add(equation);
            }
            AssignEquations();
        }

        public override void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            if ( player.GetComponent<PlayerManager>().FacedEnemies(enemies))
            {
                triggerArea.SetActive(false);
                platformCleared?.Invoke(gameObject); 
                DestroyWall(player);
            }
        }

        private void AssignEquations()
        {
            leftWall.text ="x "  + _equations[0].mathEquation; 
            rightWall.text ="x "  + _equations[1].mathEquation;
        }

        
        public void TriggerMathGate(bool isLeft,GameObject player)
        {
            canvas.SetActive(false);
            if (isLeft)
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(_equations[0].mathEquation);
            }
            else
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(_equations[1].mathEquation);
            }
        }
    }
}
