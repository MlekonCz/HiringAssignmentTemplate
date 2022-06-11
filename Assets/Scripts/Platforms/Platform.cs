using System.Collections.Generic;
using Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        
        [SerializeField] private TMP_Text enemyNumber;
        
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
            enemyNumber.text = enemies.ToString();
        }
        public void SetPool(IObjectPool<GameObject> pool)
        {
            _platformPool = pool;
        }

        public virtual void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            triggerArea.SetActive(false);
            player.GetComponentInParent<PlayerManager>().FacedEnemies(enemies);
            platformCleared?.Invoke(gameObject);
        }
        
        
        
        
    }
}