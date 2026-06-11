using System;
using UnityEngine;

namespace _02._Script.Objects
{
	public class Trapdoor : TriggerableMono
	{
		private HingeJoint _hinge;
		private void Start()
		{
			_hinge = GetComponentInChildren<HingeJoint>();
			_hinge.useLimits = true;
			JointLimits limits = _hinge.limits;
			limits.min = 0f;
			limits.max = 0f;
			_hinge.limits = limits;
		}

		public override void Trigger()
		{
			_hinge.useLimits = false;
		}
	}
}