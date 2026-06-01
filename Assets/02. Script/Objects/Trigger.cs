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
        [SerializeField] private UnityEvent<bool> onTrigger;

        private Collider[] _colliders =  new Collider[16];
        private bool _pressed = false;
        private bool _pressing = false;
    
        private void Update()
        {
            _colliders =
                Physics.OverlapBox(
                    castOffset + transform.position, 
                    castSize / 2f, 
                    Quaternion.identity,  
                    castLayer);
        
            _pressing = _colliders.Length > 0;

            if (_pressed ^ _pressing)
            {
                _pressed = _pressing;
                onTrigger?.Invoke(_pressing);
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
    }
}
