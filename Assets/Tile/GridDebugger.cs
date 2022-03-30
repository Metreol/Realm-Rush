using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridDebugger : MonoBehaviour
{
    [SerializeField] private bool coordinatesEnabledGame = false; // Set this to false/true to turn off/on coordinate labels in GAME.
    [SerializeField] private bool coordinatesEnabledEditor = false; // Set this to false/true to turn off/on coordinate labels in EDITOR.

    public bool GetCoordinatesEnabledGame()
    {
        return coordinatesEnabledGame;
    }

    public bool GetCoordinatesEnabledEditor()
    {
        return coordinatesEnabledEditor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            coordinatesEnabledGame = !coordinatesEnabledGame;
        }
    }
}
