using System;
using _02._Script.Player.Controls;
using UnityEditor;
using UnityEngine;
using UnityUtilities;

namespace _02._Script.Objects
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Vector3[] checkpoints;
        [SerializeField] private float moveSpeed;
        [SerializeField] private MovingTypes movingTypes;
        
        private Vector3 _startPos;
        private Rigidbody _rigid;

        private bool _moving = false;
        private bool _returning = false;
        private int _currentCheckpoint = 0;
        private int _targetCheckpoint = 1;

        protected virtual void Awake()
        {
            _startPos = transform.position;
        }

        protected virtual void Start()
        {
            _rigid = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            if (!_moving) return;
            
            Vector3 dir = (checkpoints[_targetCheckpoint] + _startPos - transform.position).normalized;
            _rigid.position += dir * (moveSpeed * Time.fixedDeltaTime);
            if (transform.position.ArrivedAt(checkpoints[_targetCheckpoint] + _startPos , 0.01f))
            {
                if (_returning)
                {
                    _currentCheckpoint--;
                    _targetCheckpoint = _currentCheckpoint - 1;
                    if (_currentCheckpoint <= 0)
                    {
                        _returning = false;
                        _targetCheckpoint = _currentCheckpoint + 1;
                    }
                }
                else
                {
                    _currentCheckpoint++;
                    _targetCheckpoint = _currentCheckpoint + 1;
                    if (_currentCheckpoint >= checkpoints.Length - 1)
                    {
                        _returning = true;
                        _targetCheckpoint = _currentCheckpoint - 1;
                    }
                }
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (checkpoints == null || checkpoints.Length == 0) return; // No Checkpoints
            Gizmos.color = Color.mediumSpringGreen;
            
            for (int i = 0; i < checkpoints.Length - 1; i++)
            {
                Gizmos.DrawLine(checkpoints[i] + _startPos, checkpoints[i + 1] + _startPos);
            }
        }

        public void StartMoving()
        {
            _moving = true;
        }

        public enum MovingTypes
        {
            Once, Repeat, Cycle
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (!EditorApplication.isPlaying)
            {
                _startPos = transform.position;
            }
        }
#endif
    }
}
