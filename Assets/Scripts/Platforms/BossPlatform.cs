using System;
using Core;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class BossPlatform : Platform
    {
        public event Action LevelFinished;
        [SerializeField] private GameObject _particles;

        public override void TriggerEnemyArea(GameObject player)
        {
            if (!player.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
            {
                return;
            }

            var shouldDestroy = playerManager.FacedEnemies(_enemies);
            if (!shouldDestroy)
            {
                PersistentObjects.Instance.GameManager.OnLevelFinished?.Invoke(false);
                return;
            }
            
            DestroyWall(player);
            playerManager.LevelFinished();
            _particles.SetActive(true);
            PersistentObjects.Instance.GameManager.OnLevelFinished?.Invoke(true);
        }
    }
}