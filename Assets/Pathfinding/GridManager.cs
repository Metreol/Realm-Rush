using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    [Tooltip("The coordinate of the bottom left corner of the grid.")]
    [SerializeField] private Vector2Int gridStart;
    [Tooltip("The coordinate of the top right corner of the grid.")]
    [SerializeField] private Vector2Int gridEnd;
    [Tooltip("Set to match UnityEditor snap settings.")]
    [SerializeField] private int unityGridSize = 10;

    public int UnityGridSize => unityGridSize;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    private bool gridPopulated = false;

    public Dictionary<Vector2Int, Node> Grid => grid;
    public bool IsGridPopulated => gridPopulated;

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        Node node = GetNode(coordinates);

        if (node != null)
        {
            node.isWalkable = false;
        }
    }

    public void BlockNode(Node node)
    {
        node.isWalkable = false;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.ResetNode();
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        return new Vector2Int()
        {
            x = Mathf.RoundToInt(position.x / unityGridSize),
            y = Mathf.RoundToInt(position.z / unityGridSize)
        };
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        return new Vector3()
        {
            x = coordinates.x * unityGridSize,
            z = coordinates.y * unityGridSize
        };
    }

    private void CreateGrid()
    {
        for (int x = gridStart.x; x <= gridEnd.x; x++)
        {
            for (int y = gridStart.y; y <= gridEnd.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
        gridPopulated = true;
    }
}
