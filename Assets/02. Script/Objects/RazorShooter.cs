using System;
using System.Collections;
using _02._Script.Objects;
using _02._Script.Player.Controls;
using UnityEngine;

public class RazorShooter : TriggerableMono, IInteractable
{
    [SerializeField] private Transform razorOffset;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxDistance;
    [SerializeField] private float maxLineWidth;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private float ignoreWidth;
    [SerializeField] private float triggerDelay = 0.1f;
    private LineRenderer _line;
    
    private Coroutine _lerpWidthCoroutine = null;
    

    private void Start()
    {
        _line = GetComponentInChildren<LineRenderer>();
        _line.widthCurve = new AnimationCurve(new Keyframe(0, maxLineWidth));
        _line.widthMultiplier = 0;
    }
    
    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (_lerpWidthCoroutine != null)
            StopCoroutine(_lerpWidthCoroutine);
        
        Vector3 dir = transform.rotation * Vector3.forward;
        bool isHit = Physics.Raycast(razorOffset.position, dir, out RaycastHit hit, maxDistance, layer);
        Vector3 targetPoint;
        if (isHit)
        {
            targetPoint = razorOffset.InverseTransformPoint(hit.point);

            if (hit.collider.gameObject.TryGetComponent(out ITriggerable triggerable))
            {
                StartCoroutine(TriggerCoroutine(triggerable));
            }
        }
        else
        {
            targetPoint = Vector3.forward * maxDistance;
        }
        
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, targetPoint);
        _line.widthMultiplier = 1;
        
        _lerpWidthCoroutine = StartCoroutine(LerpWidthCoroutine());
    }

    private IEnumerator LerpWidthCoroutine()
    {
        while (true)
        {
            float cur = _line.widthMultiplier;
            float t = Mathf.Clamp01(Time.deltaTime * lerpSpeed);
            float width = Mathf.Lerp(cur, 0f, t);
            _line.widthMultiplier = width;
            if (width <= ignoreWidth)
            {
                break;
            }
            
            yield return null;
        }
        
        _line.widthMultiplier = 0f;
    }

    private IEnumerator TriggerCoroutine(ITriggerable triggerable)
    {
        yield return new WaitForSeconds(triggerDelay);
        triggerable.Trigger();
    }

    public void Interact(IInteractor interactor)
    {
        Shoot();
    }

    public override void Trigger()
    {
        Shoot();
    }
}
