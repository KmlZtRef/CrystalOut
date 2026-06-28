using System;
using UnityEngine;

namespace _02._Script.UI
{
	public class TutorialCanvas : MonoBehaviour
	{
		[SerializeField] private LayerMask playerLayer;
		[SerializeField] private Vector3 boundarySize;
		[SerializeField] private Vector3 boundaryOffset;
		[SerializeField] private string animParamName;
		private Canvas _canvas;
		private Animator _anim;
		private ParticleSystem _particle;

		private int _animParamHash;

		private Camera _cam;

		private Camera MainCamera
		{
			get
			{
				if (!_cam) _cam = Camera.main;
				return _cam;
			}
		}
		
		
		private void Start()
		{
			_canvas = GetComponentInChildren<Canvas>();
			_anim = GetComponentInChildren<Animator>();
			_particle = GetComponentInChildren<ParticleSystem>();
		  	_animParamHash = Animator.StringToHash(animParamName);
		}

		private void Update()
		{
			_canvas.worldCamera ??= MainCamera;
			
			if (MainCamera)
			{
				transform.rotation = MainCamera.transform.rotation;
			}
			
			bool active = Physics.CheckBox(transform.position + boundaryOffset, boundarySize/2f, Quaternion.identity, playerLayer);
			SetTutorialActivation(active);
		}

		private void SetTutorialActivation(bool active)
		{
			_anim.SetBool(_animParamHash, active);
			
			if (active)
				_particle.Stop();
			else
				_particle.Play();
		}
		
#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.lightGoldenRod;
			Gizmos.DrawWireCube(transform.position + boundaryOffset, boundarySize);
		}

#endif
	}
}