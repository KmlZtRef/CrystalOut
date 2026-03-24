using System;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class PlayerSight : MonoBehaviour, ISight
	{
		[SerializeField] private Transform target;
		[SerializeField] private float sensitive;
		private Vector3 _eulerAngle;
		public void LookHandle(Vector2 delta)
		{
			Vector3 newAngle = _eulerAngle;
			newAngle.y += delta.x * sensitive / 100f;
			newAngle.x -= delta.y * sensitive / 100f;
			newAngle.x = Mathf.Clamp(newAngle.x, -89f, 89f);
			
			_eulerAngle = newAngle;
		}

		private void FixedUpdate()
		{
			target.rotation = Quaternion.Euler(_eulerAngle);
		}
	}
} 