using System;
using System.Data;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimatorHandler _animatorHandler;
        
        [SerializeField] private TMP_Text _playerNumber_UI;
        [SerializeField] private float _startingNumber = 1f;
        
         public float CurrentNumber;

        public delegate void CallBackType(bool playerWon);
        public event CallBackType PlayerLost;



        private void Start()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
            CurrentNumber = _startingNumber;
            UpdateUiNumber();
        }

        public void ChosenMathEquation(String mathEquation)
        {
            string number = CurrentNumber.ToString() + mathEquation;
            double result = Convert.ToDouble(new DataTable().Compute
                (number,null));
            
            Mathf.CeilToInt((int)result);
            CurrentNumber = (int)result;
            if (CurrentNumber <1){CurrentNumber = 1;}
            
            UpdateUiNumber();
        }

        public bool FacedEnemies(int numberOfEnemies)
        {
            _animatorHandler.PlayTargetAnimation("WallHit",0.2f);
            CurrentNumber -= numberOfEnemies;
            UpdateUiNumber();

            if (CurrentNumber <= 0)
            {
                GetComponent<PlayerMover>().StopPlayer();
                _animatorHandler.PlayTargetAnimation("Fall",0f);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void UpdateUiNumber()
        {
            _playerNumber_UI.text = CurrentNumber.ToString();
        }

        public void LevelFinished()
        {
            _animatorHandler.PlayTargetAnimation("Victory",0.3f);
            GetComponent<PlayerMover>().StopPlayer();
        }
    }
}