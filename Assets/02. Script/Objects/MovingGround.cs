using System;
using System.Collections;
using _02._Script.Datas;
using Unity.Cinemachine;
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
		[SerializeField] private CinemachineImpulseSource movingImpulse;
		[SerializeField] private CinemachineImpulseSource endingImpulse;
		[SerializeField] private float impulseDuration;
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip movingSound;
		[SerializeField] private AudioClip endingSound;
		[SerializeField] private CinemachineCamera focusCam;
		[SerializeField] private float camDuration;

		private Rigidbody _rigid;

		private Vector3 _direction;
		
		private bool _isReverse = true;
		private bool _arrived = true;

		private bool _useCam;
		
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
			if (!_arrived) return;
			
			_isReverse = !_isReverse;
			_arrived = false;
			
			_direction = (_isReverse ? -targetPosition : targetPosition).normalized;

			StartCoroutine(ImpulseCoroutine());
			if (_useCam) StartCoroutine(CamCoroutine());
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

		private IEnumerator ImpulseCoroutine()
		{
			audioSource.clip = movingSound;
			audioSource.Play();
			while (!_arrived)
			{
				movingImpulse.GenerateImpulse();
				yield return new WaitForSeconds(impulseDuration);
			}
			endingImpulse.GenerateImpulse();
			audioSource.Stop();
			audioSource.clip = endingSound;
			audioSource.Play();
		}

		private IEnumerator CamCoroutine()
		{
			focusCam.Priority = 3;
			yield return new WaitForSeconds(camDuration);
			focusCam.Priority = 0;
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
		public override void InjectData(SettingDataContext context)
		{
			_useCam = context.moveCamOnObjectActive;
		}
	}
}