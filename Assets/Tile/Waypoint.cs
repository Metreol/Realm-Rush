using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlacable;

    public bool IsPlacable { get { return isPlacable; } }

    private void OnMouseDown()
    {
        if (isPlacable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlacable = !isPlaced;
        }
    }
}
