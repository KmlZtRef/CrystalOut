using System;
using _02._Script.Player.Controls;
using UnityEngine;

public class CamMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed;
    [SerializeField] private Transform camOrigin;
    private Rigidbody _rigid;
    private Vector3 _velocity;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    public void MoveHandle(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _velocity = new  Vector3(direction.x, 0, direction.y);
        }
        else
        {
            _velocity = Vector3.zero;
        }
    }

    public void JumpHandle()
    {
        
    }

    public void StopMovement()
    {
        _velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3 lookingDir = camOrigin.rotation * _velocity;
			
        _rigid.linearVelocity = lookingDir.normalized * speed;
    }
}
