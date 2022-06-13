using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Walls
{

    [Serializable]
    public struct BrokenWallPartDefinition
    {
        public Vector3 Position;
        public Vector3 Scale;
    }
    
    public class Wall : MonoBehaviour
    {
        [BoxGroup("Normal Wall"), SerializeField] private GameObject _normalWall;
        [BoxGroup("Broken Wall"), SerializeField] private GameObject _brokenWall;
        [BoxGroup("Broken Wall"), SerializeField] private Transform[] _brokenWallParts;
        [BoxGroup("Broken Wall"), SerializeField] private BrokenWallPartDefinition[] _brokenWallPartsDefinitions;
        [BoxGroup("Particles"), SerializeField] private ParticleSystem _particles;

        private void Awake()
        {
            SetParts();
        }


        private void OnDisable()
        {
            _normalWall.SetActive(true);
            
            for (int i = 0; i < _brokenWallParts.Length; i++)
            {
                _brokenWallParts[i].localPosition = _brokenWallPartsDefinitions[i].Position;
                _brokenWallParts[i].localScale = _brokenWallPartsDefinitions[i].Scale;
            }
            _particles.gameObject.SetActive(false);
            _brokenWall.SetActive(false);
        }

        public void SetActive()
        {
            _normalWall.SetActive(true);
            _brokenWall.SetActive(false);
            _particles.gameObject.SetActive(false);
        }
        public void SetBroken(Vector3 position)
        {
            _normalWall.SetActive(false);
            _brokenWall.SetActive(true);
            _particles.transform.position = position;
            _particles.gameObject.SetActive(true);
        }
        private void SetParts()
        {
            _brokenWallPartsDefinitions = new BrokenWallPartDefinition[_brokenWallParts.Length];
            for (int i = 0; i < _brokenWallParts.Length; i++)
            {
                var childTransform = _brokenWallParts[i];
                _brokenWallPartsDefinitions[i] = new BrokenWallPartDefinition()
                {
                    Position = childTransform.localPosition,
                    Scale = childTransform.localScale
                };
            }
        }
    }
}