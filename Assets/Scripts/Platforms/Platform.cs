using System;
using System.Collections.Generic;
using Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        [SerializeField] protected GameObject normalWall;
        [SerializeField] private GameObject brokenWall;
        [SerializeField] private TMP_Text wallNumber;
        [SerializeField] private GameObject explosionParticle;

        [SerializeField] private float wallExplosionPower = 200f;
        

        private IObjectPool<GameObject> _platformPool;
        protected int enemies;
        
        
        
        
        public virtual void Initialize(List<EquationDefinition> equationDefinitions, int numberOfEnemies)
        {
            enemies = numberOfEnemies;
            AssignEnemyNumber();
        }
        public virtual void Initialize(int numberOfEnemies)
        {
            enemies = numberOfEnemies;
            AssignEnemyNumber();
        }
        
        
        private void AssignEnemyNumber()
        {
            wallNumber.text = enemies.ToString();
        }
        public void SetPool(IObjectPool<GameObject> pool)
        {
            _platformPool = pool;
        }

        public abstract void TriggerEnemyArea(GameObject triggerArea, GameObject player);

        protected void DestroyWall(GameObject player)
        {
            Instantiate(explosionParticle, player.transform.position, Quaternion.identity);
            normalWall.SetActive(false);
            GameObject wallPieces = Instantiate(brokenWall, normalWall.transform.position,Quaternion.identity);
            foreach (Transform child in wallPieces.transform)
            {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(wallExplosionPower,player.transform.position,7f );
                }
            }
            Destroy(wallPieces, 2f);
        }
        
        
        
    }
}