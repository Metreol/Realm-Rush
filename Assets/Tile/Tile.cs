using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlacable;

    public bool IsPlacable { get { return isPlacable; } }

    private GridManager gridManager;
    private Pathfinder pathfinder;
    private Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();

        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlacable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        Node node = gridManager.GetNode(coordinates);

        if (node != null && node.isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool towerCreationSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (towerCreationSuccessful)
            {
                gridManager.BlockNode(node);
                pathfinder.NotifyRecievers();
            }

            pathfinder.GetNewPath(); // Reset path with new world layout
        }
    }

}
