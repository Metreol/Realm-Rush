using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;

    public Vector2Int StartCoordinates => startCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;

    // Important Nodes
    private Node defaultStartNode;
    private Node destinationNode;
    private Node currentSearchNode;
    // Terrain representations
    private Dictionary<Vector2, Node> exploredNodes = new Dictionary<Vector2, Node>();
    private Queue<Node> nodesToSearch = new Queue<Node>();
    // Grid Manager
    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            defaultStartNode = gridManager.GetNode(startCoordinates);
            destinationNode = gridManager.GetNode(destinationCoordinates);
        }
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int currentCoordinates)
    {
        gridManager.ResetNodes(); // Nodes are reset so current state of the world can be assessed.
        BreadthFirstSearch(currentCoordinates);
        return BuildPath();
    }

    /* This method depends on how we calulate the path we return in BuildPath() (within GetNewPath()), in that we look back through the
     * connectedNode values starting from the destinationNode. SO if the path is blocked when this tile is set
     * temporarily to isWalkable = false then the path returned by GetNewPath() will only be 1 node long as the destination
     * node will never have had it's connectedNode set.
     */
    public bool WillBlockPath(Vector2Int coordinates)
    {
        Node node = gridManager.GetNode(coordinates);

        if (node != null)
        {
            bool previousWalkableState = node.isWalkable;
            node.isWalkable = false;
            List<Node> path = GetNewPath();
            node.isWalkable = previousWalkableState;


            if (path.Count <= 1)
            {
                GetNewPath(); // Previous path was built assuming node.isWalkable == false so we need to rebuild it with the correct value.
                return true;
            }
        }

        return false;
    }

    public void NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    private void BreadthFirstSearch(Vector2Int pathStartCoordinates)
    {
        bool isRunnning = true;
        // Clear nodesToSearch and exploredNodes to ensure up to date path is found.
        nodesToSearch.Clear();
        exploredNodes.Clear();
        Node pathStartNode = grid[pathStartCoordinates];

        nodesToSearch.Enqueue(pathStartNode);
        exploredNodes.Add(pathStartCoordinates, pathStartNode);

        while (nodesToSearch.Count > 0 && isRunnning)
        {
            currentSearchNode = nodesToSearch.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunnning = false;
            }
        }
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int nextSearchNodeCoordinates = currentSearchNode.coordinates + direction;
            Node nextSearchNode = gridManager.GetNode(nextSearchNodeCoordinates);

            if (nextSearchNode != null && !exploredNodes.ContainsKey(nextSearchNodeCoordinates) && nextSearchNode.isWalkable) {
                // Connects backwards to previous node in path. A node can connect forwards to many nodes, but backwards to only 1. (Think tree structure)
                nextSearchNode.connectedNode = currentSearchNode; 
                exploredNodes.Add(nextSearchNodeCoordinates, nextSearchNode);
                nodesToSearch.Enqueue(nextSearchNode);
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedNode != null)
        {
            currentNode = currentNode.connectedNode;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse(); // We figured out the path from destination to start, so needs to be reversed before being returned.
        return path;
    }

}
