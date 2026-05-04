using System;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class CamCatchHandler : MonoBehaviour, ICatchHandler
	{
		[SerializeField] private Transform camOrigin;
		[SerializeField] private LayerMask objectLayer;
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private float distance;
		
		private PickableObject _picked;
		public void OnInteract(IInteractable interactable)
		{
			if (interactable != null)
			{
				if (interactable is PickableObject pickable)
				{
					if (_picked != null) return;
					_picked = pickable;
					_picked.Pick();
				}
			}
		}

		public void OnDrop()
		{
			_picked?.Drop();
			_picked = null;
		}

		private void Update()
		{
			if (_picked)
			{
				Vector3 dir = camOrigin.rotation * Vector3.forward;
				Vector3 pos;
				if (Physics.Raycast(camOrigin.position, dir, out RaycastHit hit, distance, groundLayer))
				{
					pos = hit.point;
				}
				else
				{
					Vector3 direction = camOrigin.rotation * Vector3.forward * distance;
					pos = transform.position + direction;
				}
				
				_picked.transform.position = pos;
			}
		}
	}
}