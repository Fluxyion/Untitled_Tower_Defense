using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    [SerializeField]Vector2Int gridSize;
    [FormerlySerializedAs("UnityGridSize")] [SerializeField] private int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; }
    }
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    void Awake()
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
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
        
    }
    public void UnblockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = true;
        }
        
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;

        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z/ unityGridSize);
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;

    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x ; x++)
        {
            for (int y = 0 ; y < gridSize.y; y++)
            {
                Vector2Int _coordinates = new Vector2Int(x, y);
                grid.Add(_coordinates,new Node(_coordinates,true));
            }

        }
        
    }
    
}
