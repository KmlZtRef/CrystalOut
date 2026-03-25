using System;
using _02._Script.Player.Controls;
using UnityEngine;

namespace _02._Script
{
	public class PickableObject : MonoBehaviour, IInteractable
	{
		[SerializeField] private float normalPushRange;
		protected Rigidbody Rigid;
		protected Collider Collider;
		
		public float NormalPush => normalPushRange;

		private void Start()
		{
			Rigid = GetComponent<Rigidbody>();
			Collider = GetComponent<Collider>();
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

		public virtual void Fix()
		{
			
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.lawnGreen;
			Gizmos.DrawWireSphere(transform.position, normalPushRange);
		}
	}
}