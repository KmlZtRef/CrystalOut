using System;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;
using UnityUtilities;

namespace _02._Script.Player.Controls
{
	public class ObjectCatchHandler : MonoBehaviour, ICatchHandler
	{
		[SerializeField] private Transform camOrigin;
		[SerializeField] private LayerMask objectLayer; 
		[SerializeField] private LayerMask wallLayer; 
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private float distance;
		[SerializeField] private float maxPickableMass;
		[SerializeField] private float rotationAmount = 15;
		
		private NotifyValue<bool> _droppable = new NotifyValue<bool>();
		
		private PickableObject _picked;
		
		private Collider[] _colliders = new Collider[4];

		private void Start()
		{
			_droppable.OnValueChanged += HandleDroppableStateChanged;
		}

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
			if (_droppable.Value && _picked != null)
			{
				_picked.Drop();
				_picked = null;
			}
		}

		public void OnRotate(float amount)
		{
			if (_picked != null)
			{
				Vector3 rot = _picked.transform.rotation.eulerAngles;
				rot.y += amount * rotationAmount;
				_picked.transform.rotation = Quaternion.Euler(rot);
			}
		}

		private void Update()
		{
			if (!_picked)
			{
				SetDroppable(true);
				return;
			}
			
			PrecheckStyleCatch();
		}

		#region Catch Methods
		private void PrecheckStyleCatch()
		{
			// 충돌 감지 떡칠 너무 싫지만 어쩔수 었음
			
			Vector3 dir = camOrigin.rotation * Vector3.forward;

			bool boxCast = Physics.BoxCast(
				camOrigin.position,
				_picked.CastingSize,
				dir,
				out RaycastHit hitInfo,
				_picked.transform.rotation,
				distance,
				groundLayer);
			
			
			float dist;
			if (boxCast)
			{
				dist = hitInfo.distance;
			}
			else
			{
				bool rayCast = Physics.Raycast(
					camOrigin.position,
					dir,
					distance,
					groundLayer);

				// BoxCast가 실패했는데 RayCast가 성공하면 벽뚫로 판정
				if (rayCast)
				{
					// Failure
					SetDroppable(false);
					return;
				}

				dist = distance;
			}
			
			Vector3 point = camOrigin.position + dist * dir;
			
			int count = Physics.OverlapBoxNonAlloc(
				point,
				_picked.CastingSize, 
				_colliders, 
				_picked.transform.rotation, 
				groundLayer);

			if (count > 0)
			{
				// Failure
				SetDroppable(false);
				return;
			}
			
			_picked.transform.position = point;
			
			
			SetDroppable(true);
		}
		#endregion

		private void SetDroppable(bool droppable)
		{
			_droppable.Value = droppable;
			_picked?.SetVisible(droppable);
		}

		private void HandleDroppableStateChanged(bool v)
		{
			MessageBus.Publish<OnDroppableStateChanged>(new OnDroppableStateChanged(){Droppable = v});
		}
	}
}
