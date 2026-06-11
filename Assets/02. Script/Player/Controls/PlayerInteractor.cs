using System;
using System.Linq;
using _02._Script.Logics;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class PlayerInteractor : MonoBehaviour, IInteractor
	{
		[SerializeField] private Transform camOrigin;
		[SerializeField] private LayerMask objectLayer;
		[SerializeField] private float dist = 5;
		public event Action<IInteractable> OnInteractAction;

		public void InteractHandle()
		{
			if (camOrigin == null) Debug.Log($"NULL {this.name}");
			Vector3 dir = camOrigin.rotation * Vector3.forward;
			Debug.DrawRay(camOrigin.position, dir * dist, Color.red, 2f);
			Physics.Raycast(camOrigin.position, dir, out var hit, dist, objectLayer);
			if (hit.transform && hit.transform.TryGetComponent(out IInteractable obj))
			{
				obj?.Interact(this);
				OnInteractAction?.Invoke(obj);
			}
		}
	}
}