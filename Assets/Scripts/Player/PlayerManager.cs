using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private float startingNumber = 1f;
        public float currentNumber;


        private void Awake()
        {
            currentNumber = startingNumber;
        }

        public void UpdateCurrentNumber()
        {
            
        }
    }
}