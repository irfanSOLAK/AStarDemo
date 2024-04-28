using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] Transform traveler;
    [SerializeField] PathFinding pathFinding;
    [SerializeField] CanvasButtons canvasButtons;

    CharacterMovement characterMovement;

    Vector3 clickPosition;
    bool isTargetGround;
    string groundTag = "Ground";

    private void Start()
    {
        characterMovement = traveler.GetComponent<CharacterMovement>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            characterMovement.IsMoving = false;
        }
        else if (Input.GetMouseButton(0))
        {
            clickPosition = GetClickPosition();
            TeleportObjectTo(clickPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isTargetGround && !canvasButtons.IsPlacing)
            {
                pathFinding.FindShortestPath(traveler.position, transform.position);
                characterMovement.IsMoving = true;
            }


            isTargetGround = false;
        }
    }

    private Vector3 GetClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(groundTag))
        {
            isTargetGround = true;
            return hit.point;
        }

        return transform.position;
    }

    private void TeleportObjectTo(Vector3 newPosition)
    {
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}