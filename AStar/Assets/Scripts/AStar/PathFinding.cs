using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    const int straightCost = 10;
    const int diagonalCost = 14;

    GridSystem gridSystem;

    void Awake()
    {
        gridSystem = GetComponent<GridSystem>();
    }

    public void FindShortestPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = gridSystem.GetNodeFromWorldPoint(startPosition);
        Node targetNode = gridSystem.GetNodeFromWorldPoint(targetPosition);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openSet);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                TracePathBack(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in gridSystem.GetNeighbours(currentNode))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.GCost + CalculateDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCostToNeighbour;
                    neighbour.HCost = CalculateDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].FCost < lowestFCostNode.FCost || (nodeList[i].FCost == lowestFCostNode.FCost && nodeList[i].HCost < lowestFCostNode.HCost))
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    void TracePathBack(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();

        gridSystem.Path = path;
    }

    int CalculateDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        return diagonalCost * Mathf.Min(distanceX, distanceY) + straightCost * Mathf.Abs(distanceX - distanceY);
    }
}
