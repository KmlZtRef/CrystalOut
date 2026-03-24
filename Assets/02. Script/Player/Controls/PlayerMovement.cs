using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class PlayerMovement : MonoBehaviour, IMovement
	{
		[SerializeField] private float speed = 15;
		[SerializeField] private float jumpPower = 5;
		[SerializeField] private Transform referenceTransform;
		private GroundChecker _groundChecker;
		private Rigidbody _rigid;
		private Vector3 _velocity;

		private void Start()
		{
			_rigid =  GetComponent<Rigidbody>();
			_groundChecker = GetComponentInChildren<GroundChecker>();
		}

		public void MoveHandle(Vector2 direction)
		{
			if (direction != Vector2.zero)
			{
				_velocity = new  Vector3(direction.x, 0, direction.y);
			}
			else
			{
				_velocity = Vector3.zero;
			}
		}

		public void JumpHandle()
		{
			if (_groundChecker.IsGround)
				_rigid.AddForce(0, jumpPower, 0, ForceMode.Impulse);
		}

		public void StopMovement()
		{
			_velocity = Vector3.zero;
		}

		private void FixedUpdate()
		{
			Vector3 lookingDir = referenceTransform.rotation * _velocity;
			Vector2 dir = new Vector2(lookingDir.x, lookingDir.z).normalized;
			float yVelocity = _rigid.linearVelocity.y;
			
			_rigid.linearVelocity = new  Vector3(dir.x * speed, yVelocity, dir.y * speed);
		}
	}
}