using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{
    [SerializeField] private Color freeSpaceColor = Color.blue;
    [SerializeField] private Color takenSpaceColor = Color.red;

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
    }

    private void Update()
    {
        ToggleLabels();

        if (!Application.isPlaying)
        {
            UpdateCoordinatesLabel();
            UpdateObjectNames();
            label.enabled = false; // Set this to false/true to turn off/on coordinate labels in Editor.
        }

        LabelColor();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            label.enabled = !label.enabled;
        }
    }

    private void UpdateCoordinatesLabel()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectNames()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void LabelColor()
    {
        if (waypoint.IsPlacable)
        {
            label.color = freeSpaceColor;
        }
        else
        {
            label.color = takenSpaceColor;
        }
    }
}
