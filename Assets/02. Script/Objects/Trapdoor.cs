using System;
using System.Collections;
using _02._Script.Datas;
using Unity.Cinemachine;
using UnityEngine;

namespace _02._Script.Objects
{
	public class Trapdoor : TriggerableMono
	{
		[SerializeField] private CinemachineCamera focusCam;
		[SerializeField] private float camDuration;
		private HingeJoint _hinge;
		private bool _useCam;
		
		private void Start()
		{
			_hinge = GetComponentInChildren<HingeJoint>();
			
			_hinge.useLimits = true;
			JointLimits limits = _hinge.limits;
			limits.min = 0f;
			limits.max = 0f;
			_hinge.limits = limits;

			focusCam.Priority = 0;
		}

		public override void Trigger()
		{
			_hinge.useLimits = false;
			if (_useCam)
				StartCoroutine(TriggerCoroutine());
		}

		private IEnumerator TriggerCoroutine()
		{
			focusCam.Priority = 3;
			yield return new WaitForSeconds(camDuration);
			focusCam.Priority = 0;
		}

		public override void InjectData(SettingDataContext context)
		{
			_useCam = context.moveCamOnObjectActive;
		}
	}
}