using System;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class ObjectCatchHandler : MonoBehaviour, ICatchHandler
	{
		[SerializeField] private Transform handOrigin;
		private PickableObject _picked;
		public void OnInteract(IInteractable interactable)
		{
			if (interactable != null)
			{
				if (interactable is PickableObject pickable)
				{
					_picked?.Drop();
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
				_picked.transform.position = handOrigin.position;
		}
	}
}