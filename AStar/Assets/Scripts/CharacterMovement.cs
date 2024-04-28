using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] GridSystem gridSystem;

    private int currentWayPointIndex = 0;

    private bool ismoving;

    public bool IsMoving
    {
        get { return ismoving; }
        set
        {
            ismoving = value;

            if (!ismoving)
            {
                currentWayPointIndex = 0;
                gridSystem.Path = null;
            }
        }
    }

    void Update()
    {
        if (IsMoving)
        {
            MoveToNextWaypoint();
        }

    }

    void MoveToNextWaypoint()
    {
        if (gridSystem.Path == null)
            return;

        Node currentWayPoint = gridSystem.Path[currentWayPointIndex];
        Vector3 currentWaypointPosition = new Vector3(currentWayPoint.WorldPosition.x, transform.position.y, currentWayPoint.WorldPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypointPosition, Time.deltaTime * moveSpeed);

        if (transform.position == currentWaypointPosition)
        {
            currentWayPointIndex++;

            if (currentWayPointIndex >= gridSystem.Path.Count)
            {
                IsMoving = false;
            }

        }
    }
}
