using System;
using UnityEngine;
using UnityUtilities;

[RequireComponent(typeof(Animator))]
public class LoadingUI : UnbreakingSingleton<LoadingUI>
{
    private Animator _anim;
    private int _paramHash;
    private bool _loading = false;
    
    public bool IsLoading => _loading;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _anim = GetComponent<Animator>();
        _paramHash = Animator.StringToHash("Open");
    }

    public void Open()
    {
        _anim.SetBool(_paramHash, true);
        _loading = true;
    }

    public void Close()
    {
        _anim.SetBool(_paramHash, false);
        _loading = true;
    }

    public void AnimationEnd()
    {
        _loading = false;
    }
}
