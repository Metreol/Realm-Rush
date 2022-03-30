using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;    
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f); // Orange

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;
    private GridDebugger gridDebugger;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        gridDebugger = FindObjectOfType<GridDebugger>();

        if (Application.isPlaying)
        {
            label.enabled = gridDebugger.GetCoordinatesEnabledGame();
        }
        else
        {
            label.enabled = gridDebugger.GetCoordinatesEnabledEditor();
        }
    }

    private void Start()
    {
        if (gridManager == null)
        {
            Debug.LogError("There is no GridManager object in the scene");
            return;
        }
        else if (!gridManager.IsGridPopulated)
        {
            Debug.LogError("GridManager has not been populated");
            return;
        }
    }

    private void Update()
    {
        ToggleLabels();
        UpdateCoordinatesLabel();
        UpdateObjectNames();
        LabelColor();
    }

    private void ToggleLabels()
    {
        if (Application.isPlaying)
        {
            label.enabled = gridDebugger.GetCoordinatesEnabledGame();
        }
        else
        {
            label.enabled = gridDebugger.GetCoordinatesEnabledEditor();
        }
    }

    private void UpdateCoordinatesLabel()
    {
        if (gridManager == null)
        {
            return;
        }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectNames()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void LabelColor()
    {
        Node node = gridManager.GetNode(coordinates);

        if (node != null)
        {
            if (!node.isWalkable)
            {
                label.color = blockedColor;
            }
            else if (node.isPath)
            {
                label.color = pathColor;
            }
            else if (node.isExplored)
            {
                label.color = exploredColor;
            }
            else
            {
                label.color = defaultColor; // Walkable, but not path and not explored.
            }
        } 
    }
}
