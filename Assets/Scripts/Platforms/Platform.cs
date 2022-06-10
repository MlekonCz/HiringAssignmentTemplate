using System.Collections.Generic;
using Definitions;
using Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [HideIf("isFinalPlatform", true)]
        [SerializeField] private GameObject canvas;
        [HideIf("isFinalPlatform", true)]
        [SerializeField] private TMP_Text leftWall;
        [HideIf("isFinalPlatform", true)]
        [SerializeField] private TMP_Text rightWall;
        
        [SerializeField] private TMP_Text enemyNumber;

        private List<EquationDefinition> equations = new List<EquationDefinition>();
        private EquationProvider _equationProvider;

        private int enemies;

        [SerializeField] private bool isFinalPlatform = false;


        public void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            enemies = numberOfEnemies;
            equations.Clear();
            foreach (var equation in equationDefinitions)
            {
                equations.Add(equation);
            }
            AssignEquations();
            AssignEnemyNumber();
        }
        public void Initialize(int numberOfEnemies)
        {
            enemies = numberOfEnemies;
            AssignEnemyNumber();
        }


        private void AssignEquations()
        {
            leftWall.text ="x "  + equations[0].mathEquation; 
            rightWall.text ="x "  + equations[1].mathEquation;
        }

        private void AssignEnemyNumber()
        {
            enemyNumber.text = enemies.ToString();
        }
        public void TriggerMathGate(bool isLeft,GameObject player)
        {
            canvas.SetActive(false);
            if (isLeft)
            {
                player.GetComponentInParent<PlayerManager>().ChosenMathEquation(equations[0].mathEquation);
            }
            else
            {
                player.GetComponentInParent<PlayerManager>().ChosenMathEquation(equations[1].mathEquation);
            }
        }
        public void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            triggerArea.SetActive(false);
            player.GetComponentInParent<PlayerManager>().FacedEnemies(enemies);
            if (isFinalPlatform)
            {
                //Todo Winning logic
            }
            
        }
    }
}
