using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClearUIAnim : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
    }
}
