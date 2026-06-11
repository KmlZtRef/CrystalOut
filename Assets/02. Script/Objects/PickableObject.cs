using System;
using _02._Script.Player.Controls;
using UnityEngine;

namespace _02._Script
{
	public class PickableObject : MonoBehaviour, IInteractable
	{
		[SerializeField] private float castingRadius;
		[SerializeField] private Vector3 castingSize;
		protected Rigidbody Rigid;
		protected Collider Collider;
		protected MeshRenderer[] Renderers;
		
		public float CastingRadius => castingRadius;
		public Vector3 CastingSize => castingSize;
		public float Mass => Rigid.mass;

		private void Start()
		{
			Rigid = GetComponent<Rigidbody>();
			Collider = GetComponent<Collider>();
			Renderers = GetComponentsInChildren<MeshRenderer>();
		}

		public virtual void Interact(IInteractor interactor)
		{
		}

		public virtual void Pick()
		{
			Rigid.useGravity = false;
			Collider.enabled = false;
			
			Rigid.linearVelocity = Vector3.zero;
			Rigid.angularVelocity = Vector3.zero;
		}

		public virtual void Drop()
		{
			Rigid.useGravity = true;
			Collider.enabled = true;
			
			Rigid.linearVelocity = Vector3.zero;
			Rigid.angularVelocity = Vector3.zero;
		}

		public virtual void SetVisible(bool visible)
		{
			foreach (var r in Renderers)
			{
				r.enabled = visible;
			}
		}

		private void OnDrawGizmosSelected()
		{
			// Gizmos.color = Color.lawnGreen;
			// Gizmos.DrawWireCube(transform.position, castingRadius * 2);
		}
	}
}