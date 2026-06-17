using System;
using System.Collections;
using UnityEngine;

namespace _02._Script.Objects
{
    public class ClearAreaAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem chargeParticle;
        [SerializeField] private ParticleSystem particle;
        
        public event Action OnAnimationEnd;

        private void Awake()
        {
            particle ??= GetComponent<ParticleSystem>();
        }

        public void Activate()
        {
            StartCoroutine(ActivateCoroutine());
        }

        private IEnumerator ActivateCoroutine()
        {
            chargeParticle.Play();
            yield return new WaitForSeconds(0.1f);
            particle.Stop();
            yield return new WaitForSeconds(3f);
            AnimationEnded();
        }

        public void AnimationEnded()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}
