using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // Allows an object of this type to be visible in unity inspector even though it doesn't inherit from MonoBehaviour (Need to look up how this works)
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedNode;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }

    public void ResetNode()
    {
        isExplored = false;
        isPath = false;
        connectedNode = null;
    }
}
