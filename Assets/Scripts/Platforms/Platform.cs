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

        [SerializeField] private float wallExplosionPower = 150f;

        private IObjectPool<GameObject> _platformPool;
        protected int enemies;
        
        public delegate void CallBackType(GameObject platform);
        public event CallBackType platformCleared;
        
        
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

        public virtual void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            triggerArea.SetActive(false);
            if ( player.GetComponent<PlayerManager>().FacedEnemies(enemies))
            { 
                platformCleared?.Invoke(gameObject); 
                DestroyWall(player);
            }
            
            
        }

        private void DestroyWall(GameObject player)
        {
            normalWall.SetActive(false);
            GameObject wallPieces = Instantiate(brokenWall, normalWall.transform.position,Quaternion.identity);
            foreach (Transform child in wallPieces.transform)
            {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(wallExplosionPower,player.transform.position,7f );
                }
            }
            Destroy(wallPieces, 1.5f);
        }
        
        
        
    }
}