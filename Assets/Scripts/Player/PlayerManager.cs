using System;
using System.Data;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimatorHandler _animatorHandler;
        
        [SerializeField] private TMP_Text playerNumber_UI;
        [SerializeField] private float startingNumber = 1f;
        public float currentNumber;

        public delegate void CallBackType(bool playerWon);
        public event CallBackType playerLost;


        private void Awake()
        {
            currentNumber = startingNumber;
            UpdateUiNumber();
        }

        private void Start()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void ChosenMathEquation(String mathEquation)
        {
            string number = currentNumber.ToString() + mathEquation;
            Debug.Log(mathEquation);
            double result = Convert.ToDouble(new DataTable().Compute
                (number,null));
            
            Mathf.CeilToInt((int)result);
            Mathf.Clamp((int)result, 1, Mathf.Infinity);
            currentNumber = (int)result;
            
            UpdateUiNumber();
        }

        public bool FacedEnemies(int numberOfEnemies)
        {
            _animatorHandler.PlayTargetAnimation("WallHit");
            currentNumber -= numberOfEnemies;
            UpdateUiNumber();

            if (currentNumber <= 0)
            {
                playerLost?.Invoke(false);
                return false;
            }

            return true;
        }
        private void UpdateUiNumber()
        {
            playerNumber_UI.text = currentNumber.ToString();
        }
    }
}