using System;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;
using UnityEngine.UI;

namespace _02._Script.UI
{
	public class Crosshair : MonoBehaviour
	{
		[SerializeField] private Image crosshair;

		[SerializeField] private Sprite basicSprite;
		[SerializeField] private Sprite limitedSprite;

		private void Start()
		{
			MessageBus.Subscribe<OnDroppableStateChanged>(HandleOnDroppableStateChanged);
		}

		private void OnDestroy()
		{
			MessageBus.Unsubscribe<OnDroppableStateChanged>(HandleOnDroppableStateChanged);
		}

		private void HandleOnDroppableStateChanged(OnDroppableStateChanged param)
		{
			crosshair.sprite = param.Droppable ? basicSprite : limitedSprite;
		}
	}
}