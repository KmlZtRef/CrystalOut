using System.Collections;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class PlayerMovement : MonoBehaviour, IMovement
	{
		[SerializeField] private float speed = 15;
		[SerializeField] private float acceleration = 10;
		[SerializeField] private float jumpPower = 5;
		[SerializeField] private float gravityIncreaseAmount = 4f;
		[SerializeField] private Transform referenceTransform;
		private GroundChecker _groundChecker;
		private Rigidbody _rigid;
		private Vector3 _targetVelocity;
		private Vector2 _currentVelocity;

		private void Start()
		{
			_rigid =  GetComponent<Rigidbody>();
			_groundChecker = GetComponentInChildren<GroundChecker>();
		}

		public void MoveHandle(Vector2 direction)
		{
			if (direction != Vector2.zero)
			{
				_targetVelocity = new  Vector3(direction.x, 0, direction.y);
			}
			else
			{
				_targetVelocity = Vector3.zero;
			}
		}

		public void JumpHandle()
		{
			if (_groundChecker.IsGround)
			{
				_rigid.linearVelocity = new Vector3(_rigid.linearVelocity.x, jumpPower, _rigid.linearVelocity.z);
			}
		}

		public void StopMovement()
		{
			_targetVelocity = Vector3.zero;
		}

		private void FixedUpdate()
		{
			Vector3 lookingDir = referenceTransform.rotation * _targetVelocity;
			Vector2 dir = new Vector2(lookingDir.x, lookingDir.z).normalized;
			_currentVelocity = Vector2.Lerp(_currentVelocity, dir, acceleration * Time.fixedDeltaTime);
			
			float yVelocity = _rigid.linearVelocity.y;

			if (yVelocity < 0)
			{
				_rigid.AddForce(Vector3.down * gravityIncreaseAmount, ForceMode.Acceleration);
			}
			
			_rigid.linearVelocity = new Vector3(_currentVelocity.x * speed, yVelocity, _currentVelocity.y * speed);
		}
	}
}