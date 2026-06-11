using System;
using UnityEngine;
using UnityEngine.Events;

namespace _02._Script.Objects
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private LayerMask castLayer;
        [SerializeField] private Rigidbody triggerRigid;
        [SerializeField] private Transform clampPoint;
        [SerializeField] private float resistancePower;
        [SerializeField] private Vector3 castOffset;
        [SerializeField] private Vector3 castSize;
        [Space(20)]
        [SerializeField] private TriggerDetectionMode detectionMode;
        [SerializeField] private TriggerableMono[] triggerableObjects;
        
        // [Header("Legacy")]
        // [SerializeField] private UnityEvent<bool> onTrigger;
        
        // 굳이 배열 길이를 길게 할 필요가 있나
        private readonly Collider[] _colliders =  new Collider[1];
        
        private bool _pressed = false; // 이전 프레임
        private bool _pressing = false; // 현재 프레임

        // 한번 이상 감지되었는가
        private bool _detected = false;
    
        private void Update()
        {
            Physics.OverlapBoxNonAlloc(
                castOffset + transform.position,
                castSize / 2f,
                _colliders,
                Quaternion.identity,
                castLayer);
        
            _pressing = _colliders[0]; // 뭐라도 감지되면 True

            bool pressedThisFrame = !_pressed && _pressing;
            bool releasedThisFrame = _pressed && !_pressing;
            bool stateChanged = pressedThisFrame || releasedThisFrame;

            bool detectOnce = (detectionMode & TriggerDetectionMode.Once) != 0;
            bool detectOnPress =  (detectionMode & TriggerDetectionMode.OnPress) != 0;
            bool detectOnRelease = (detectionMode & TriggerDetectionMode.OnRelease) != 0;

            if (stateChanged && !(_detected && detectOnce))
            {
                if ((pressedThisFrame && detectOnPress) || (pressedThisFrame && detectOnRelease))
                {
                    InvokeTriggerableObjects();
                    _pressed = _pressing;
                    _detected = true;
                }
            }
        }

        private void FixedUpdate()
        {
            triggerRigid.useGravity = _pressing;
            float power = clampPoint.position.y - triggerRigid.position.y;
            triggerRigid.AddForce(0, power * resistancePower, 0, ForceMode.Force);
            if (triggerRigid.position.y > clampPoint.position.y)
                triggerRigid.position = clampPoint.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.coral;
            Gizmos.DrawWireCube(castOffset + transform.position, castSize);
        }

        private void InvokeTriggerableObjects()
        {
            foreach (TriggerableMono triggerable in triggerableObjects)
            {
                triggerable.Trigger();
            }
        }
        
        [Flags]
        private enum TriggerDetectionMode : byte
        {
            OnPress = 1 << 0,
            OnRelease = 1 << 1,
            Once = 1 << 2,
        }
    }
}
