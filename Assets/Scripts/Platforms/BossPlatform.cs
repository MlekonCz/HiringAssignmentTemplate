using System;
using Player;
using UnityEngine;

namespace Platforms
{
    public class BossPlatform : Platform
    {
        public event Action levelFinished;
        [SerializeField] private GameObject particles;



        public override void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            
            if (player.GetComponent<PlayerManager>().FacedEnemies(enemies))
            {
                particles.SetActive(true);
                player.GetComponent<PlayerManager>().LevelFinished();
                triggerArea.SetActive(false);
                levelFinished?.Invoke();
                DestroyWall(player);
            }
        }
    }
}