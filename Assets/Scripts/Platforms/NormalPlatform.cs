using System.Collections.Generic;
using Core;
using Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class NormalPlatform : Platform
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private TMP_Text _leftWall;
        [SerializeField] private TMP_Text _rightWall;
        
        private List<EquationDefinition> _equations = new List<EquationDefinition>();
        
        private void OnEnable()
        {
            _canvas.SetActive(true);
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

        public override void TriggerEnemyArea(GameObject player)
        {
            if (!player.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
            {
                return;
            }

            var shouldDestroy = playerManager.FacedEnemies(_enemies);
            if (!shouldDestroy)
            {
                PersistentObjects.Instance.GameManager.OnLevelFinished?.Invoke(false);
                return;
            }
            
           
            _platformManager.OnPlatformReleased?.Invoke(this);
            DestroyWall(player);
        }

        private void AssignEquations()
        {
            _leftWall.text ="x "  + _equations[0].MathEquation; 
            _rightWall.text ="x "  + _equations[1].MathEquation;
        }

        
        public void TriggerMathGate(bool isLeft,GameObject player)
        {
            _canvas.SetActive(false);
            if (isLeft)
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(_equations[0].MathEquation);
            }
            else
            {
                player.GetComponent<PlayerManager>().ChosenMathEquation(_equations[1].MathEquation);
            }
        }
    }
}
