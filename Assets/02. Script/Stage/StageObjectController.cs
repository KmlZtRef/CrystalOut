using System;
using _02._Script.Objects;
using UnityEngine;

public class StageObjectController : MonoBehaviour
{
    [SerializeField] private Transform clearAreaLocation;
    [SerializeField] private GameObject clearAreaPrefab;
    [SerializeField] private ClearInteraction clearArea;
    
    public ClearInteraction ClearArea => clearArea;

    public void Init()
    {
        clearArea = Instantiate(
                    clearAreaPrefab,
                    clearAreaLocation.position, 
                    clearAreaLocation.rotation, 
                    clearAreaLocation)
                .GetComponent<ClearInteraction>();
    }

    private void OnDrawGizmosSelected()
    {
        if (!clearAreaLocation) return;
        Vector3 size = new Vector3(3, 5, 3);
        Vector3 loc = clearAreaLocation.position + new Vector3(0, size.y / 2f, 0);
        Gizmos.color = Color.blueViolet;
        Gizmos.DrawWireCube(loc, size);
    }
}
