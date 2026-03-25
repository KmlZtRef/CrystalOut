using System;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class GroundChecker : MonoBehaviour
	{
		[SerializeField] private Vector3 offset;
		[SerializeField] private Vector3 size;
		[SerializeField] private LayerMask groundLayer;
		private Collider[] _colliders = new Collider[1];
		public bool IsGround
		{
			get
			{
				int c = Physics.OverlapBoxNonAlloc(
					transform.position + offset, 
					size / 2f, 
					_colliders, 
					Quaternion.identity, 
					groundLayer);
				
				return c > 0;
			}
		}
		
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.crimson;
			Gizmos.DrawWireCube(transform.position + offset, size);
		}
	}
}