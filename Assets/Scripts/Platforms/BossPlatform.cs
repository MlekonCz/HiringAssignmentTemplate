using System;
using Player;
using UnityEngine;

namespace Platforms
{
    public class BossPlatform : Platform
    {
        public event Action levelFinished;
        
        
        
        public override void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            
            if (player.GetComponent<PlayerManager>().FacedEnemies(enemies))
            {
                player.GetComponent<PlayerManager>().LevelFinished();
                triggerArea.SetActive(false);
                levelFinished?.Invoke();
                DestroyWall(player);
            }
        }
    }
}