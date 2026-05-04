using System;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class ObjectCatchHandler : MonoBehaviour, ICatchHandler
	{
		[SerializeField] private Transform camOrigin;
		[SerializeField] private LayerMask objectLayer;
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private float distance;
		[SerializeField] private float maxPickableMass;
		private PickableObject _picked;
        
		public void OnInteract(IInteractable interactable)
		{
			switch (interactable)
			{
				case PickableObject pickable:
				{
					if (pickable.Mass <= maxPickableMass)
					{
						_picked?.Drop();
						_picked = pickable;
						_picked.Pick();
					}

					break;
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
			if (!_picked) return;
			
			Vector3 dir = camOrigin.rotation * Vector3.forward;
			float dist;
			if (Physics.BoxCast(camOrigin.position, _picked.CastingSize, dir, out RaycastHit hit, _picked.transform.rotation, distance, groundLayer))
			{
				dist = hit.distance;
			}
			else
			{
				dist = distance;
			}
			Vector3 pos = camOrigin.position + dist * dir;

			_picked.transform.position = pos;
		}
	}
}
