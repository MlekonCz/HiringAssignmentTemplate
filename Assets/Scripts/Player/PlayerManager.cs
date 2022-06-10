using System;
using System.Data;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerNumber_UI;
        [SerializeField] private float startingNumber = 1f;
        public float currentNumber;


        private void Awake()
        {
            currentNumber = startingNumber;
            UpdateUiNumber();
        }

       
        public void ChosenMathEquation(String mathEquation)
        {
            string number = currentNumber.ToString() + mathEquation;
            Debug.Log(mathEquation);
            double result = Convert.ToDouble(new DataTable().Compute
                (number,null));

            
            currentNumber = Mathf.CeilToInt((int)result);
            
            UpdateUiNumber();
        }

        public void FacedEnemies(int numberOfEnemies)
        {
            currentNumber -= numberOfEnemies;
            UpdateUiNumber();
        }
        private void UpdateUiNumber()
        {
            playerNumber_UI.text = currentNumber.ToString();
        }
    }
}