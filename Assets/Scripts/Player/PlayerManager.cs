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
            currentNumber = (int)result;
            if (currentNumber <1)
            {
                currentNumber = 1;
            }
            
            UpdateUiNumber();
        }

        public bool FacedEnemies(int numberOfEnemies)
        {
            _animatorHandler.PlayTargetAnimation("WallHit",0.2f);
            currentNumber -= numberOfEnemies;
            UpdateUiNumber();

            if (currentNumber <= 0)
            {
                GetComponent<PlayerMover>().StopPlayer();
                _animatorHandler.PlayTargetAnimation("Fall",0f);
                playerLost?.Invoke(false);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void UpdateUiNumber()
        {
            playerNumber_UI.text = currentNumber.ToString();
        }

        public void LevelFinished()
        {
            _animatorHandler.PlayTargetAnimation("Victory",0.3f);
            GetComponent<PlayerMover>().StopPlayer();
        }
    }
}