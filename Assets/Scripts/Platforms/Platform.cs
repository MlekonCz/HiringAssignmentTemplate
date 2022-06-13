using System;
using System.Collections.Generic;
using Core;
using Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Walls;

namespace Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wallNumber;
        [SerializeField] private float _wallExplosionPower = 200f;
        
        
        [SerializeField] protected Wall _wall;

        protected int _enemies;
        
        protected PlatformManager _platformManager;
        
        public virtual void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            _enemies = numberOfEnemies;
            AssignEnemyNumber();
        }
        public virtual void Initialize(int numberOfEnemies)
        {
            _enemies = numberOfEnemies;
            AssignEnemyNumber();
        }


        private void AssignEnemyNumber()
        {
            _wallNumber.text = _enemies.ToString();
        }
   
        public abstract void TriggerEnemyArea(GameObject player);

        protected void DestroyWall(GameObject player)
        {
            _wall.SetBroken(player.transform.position);
            foreach (Transform child in _wall.transform)
            {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(_wallExplosionPower,player.transform.position,7f );
                }
            }
        }


        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
            transform.rotation = Quaternion.identity;
        }

        public void SetManager(PlatformManager platformManager)
        {
            _platformManager = platformManager;
        }
    }
}