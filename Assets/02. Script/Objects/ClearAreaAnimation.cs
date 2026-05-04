using System;
using UnityEngine;

namespace _02._Script.Objects
{
    public class ClearAreaAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem chargeParticle;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Animator animator;

        private int _animActiveHash;
        
        public event Action OnAnimationEnd;

        private void Awake()
        {
            particle ??= GetComponent<ParticleSystem>();
            animator ??= GetComponent<Animator>();
            _animActiveHash = Animator.StringToHash("Activate");
        }

        public void ActivateAnimation()
        {
            animator.SetTrigger(_animActiveHash);
        }

        public void ChargeAnimation()
        {
            chargeParticle.Play();
        }

        public void AnimationEnded()
        {
            OnAnimationEnd?.Invoke();
            chargeParticle.Stop();
            particle.Play();
        }
    }
}
