using System;
using UnityEngine;

public class RazorShooter : MonoBehaviour
{
    [SerializeField] private Transform razorOffset;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxDistance;
    private LineRenderer _line;
    

    private void Start()
    {
        _line = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        Vector3 dir = transform.rotation * Vector3.forward;
        bool isHit = Physics.Raycast(razorOffset.position, dir, out RaycastHit hit, maxDistance, layer);
        Vector3 targetPoint;
        if (isHit)
        {
            targetPoint = razorOffset.InverseTransformPoint(hit.point);
        }
        else
        {
            targetPoint = Vector3.forward * maxDistance;
        }
        
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, targetPoint);
    }
}
