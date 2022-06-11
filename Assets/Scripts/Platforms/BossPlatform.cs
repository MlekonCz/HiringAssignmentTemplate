using System;
using Player;
using TMPro;
using UnityEngine;

namespace Platforms
{
    public class BossPlatform : Platform
    {
        public event Action levelFinished;
        
        
        
        public override void TriggerEnemyArea(GameObject triggerArea, GameObject player)
        {
            triggerArea.SetActive(false);
            if (player.GetComponentInParent<PlayerManager>().FacedEnemies(enemies))
            {
                levelFinished?.Invoke();
            }
        }
    }
}