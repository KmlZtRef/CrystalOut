using System;
using UnityEngine;

namespace _02._Script.Objects
{
	[RequireComponent(typeof(Rigidbody))]
	public class MovingGround : TriggerableMono
	{
		[SerializeField] private Vector3 startPosition;
		[SerializeField] private Vector3 targetPosition;
		[SerializeField] private float moveSpeed;
		[SerializeField] private Vector3 gizmoBoxSize = new Vector3(4, 4, 4);

		private Rigidbody _rigid;

		private Vector3 _direction;
		
		private bool _isReverse = true;
		private bool _arrived = true;
		
		private void Start()
		{
			_rigid = GetComponent<Rigidbody>();
			_rigid.useGravity = false;
			_rigid.isKinematic = true;
			
			UpdateStartPosition();
		}
		
		[ContextMenu("UpdateStartPosition")]
		private void UpdateStartPosition()
		{
			startPosition = transform.position;
		}
		
		[ContextMenu("Trigger")]
		public override void Trigger()
		{
			_isReverse = !_isReverse;
			_arrived = false;
			
			_direction = (_isReverse ? -targetPosition : targetPosition).normalized;
		}

		private void FixedUpdate()
		{
			if (_arrived) return;
			
			Vector3 dest = _isReverse? startPosition : (startPosition + targetPosition);
			Vector3 newDir = (dest - transform.position).normalized;
			if (Vector3.Dot(_direction, newDir) <= 0) // Arrived
			{
				_rigid.MovePosition(dest);
				_arrived = true;
			}
			else
			{
				_rigid.MovePosition(_rigid.position + newDir * (moveSpeed * Time.fixedDeltaTime));
			}
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			UpdateStartPosition();
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.mediumSpringGreen;
			Gizmos.DrawLine(startPosition, startPosition + targetPosition);
			Gizmos.DrawWireCube(startPosition, gizmoBoxSize);
			Gizmos.DrawWireCube(startPosition + targetPosition, gizmoBoxSize);
		}
#endif
		
	}
}