using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public Transform traveler;
    public LayerMask unwalkableMask;
    public Vector2 gridSize;
    public float nodeRadius;
    public List<Node> Path { get; set; }

    Node[,] gridNodes;
    float nodeDiameter;
    int gridSizeX, gridSizeY;


    [SerializeField] Color walkableNodeColor;
    [SerializeField] Color unwalkableNodeColor;
    [SerializeField] Color pathNodeColor;
    [SerializeField] Color travelerNodeColor;

    private const float nodeOverlap = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        ConfigureGrid();
    }

    public void ConfigureGrid()
    {
        gridNodes = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                gridNodes[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;
        float percentY = (worldPosition.z + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return gridNodes[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(gridNodes[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (gridNodes != null)
        {
            foreach (Node node in gridNodes)
            {
                Color gizmoColor = node.IsWalkable ? walkableNodeColor : unwalkableNodeColor;

                if (Path != null && Path.Contains(node))
                {
                    gizmoColor = pathNodeColor;
                }

                if (GetNodeFromWorldPoint(traveler.position) == node)
                {
                    gizmoColor = travelerNodeColor;
                }

                Gizmos.color = gizmoColor;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - nodeOverlap));
            }
        }
    }
}