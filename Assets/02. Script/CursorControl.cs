using System;
using UnityEngine;
using UnityUtilities;

public class CursorControl : UnbreakingSingleton<CursorControl>
{
	[SerializeField] private bool hideCursorOnAwake = false;

	protected override void Awake()
	{
		base.Awake();
		
		if (hideCursorOnAwake)
			HideCursor();
	}

	public void HideCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void ShowCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}

