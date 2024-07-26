using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    Color defaultColor = Color.white;
    Color blockedColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.magenta;
    private GridManager gridManager;
    

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }
        SetLabelColor();
        ToggleLabels();
        
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if(gridManager==null){return;}
        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return;}
        
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
            label.color = defaultColor;
        }
        
    }

    private void DisplayCoordinates()
    {
        if(gridManager==null){return;}
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/ gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
