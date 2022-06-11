using System;
using UnityEngine;

namespace Player
{
    public class AnimatorHandler : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayTargetAnimation(string targetAnim, float transitionDuration)
        {
            _animator.CrossFade(targetAnim, transitionDuration);
        }
    }
}