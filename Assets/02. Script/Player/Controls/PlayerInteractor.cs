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
			Vector3 dir = camOrigin.rotation * Vector3.forward;
			Debug.DrawRay(camOrigin.position, dir * dist, Color.red, 2f);
			RaycastHit[] hits = Physics.RaycastAll(camOrigin.position, dir, dist, objectLayer);
			if (hits.Length > 0)
			{
				IInteractable obj = hits[0].transform.GetComponent<IInteractable>(); // first Interactable
				obj?.Interact(this);
				OnInteractAction?.Invoke(obj);
			}
		}
	}
}