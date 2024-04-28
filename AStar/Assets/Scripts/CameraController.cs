using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 50f;

    bool isFreeView;

    // These values created arbitrarily
    Vector3 upperViewPosition = new Vector3(6f, 19f, 0);
    Quaternion upperViewRotation = Quaternion.Euler(90, 0, 0);

    Vector3 freeViewPosition = new Vector3(7f, 3f, -17f);
    Quaternion freeViewRotation = Quaternion.Euler(0, 0, 0);

    private const KeyCode forwardKey = KeyCode.W;
    private const KeyCode backwardKey = KeyCode.S;
    private const KeyCode leftKey = KeyCode.A;
    private const KeyCode rightKey = KeyCode.D;
    private const KeyCode rotateLeftKey = KeyCode.Q;
    private const KeyCode rotateRightKey = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        if (isFreeView)
        {
            MoveCamera();
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        if (Input.GetKey(rotateRightKey))
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (Input.GetKey(rotateLeftKey))
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    private void MoveCamera()
    {
        Vector3 moveDirection = Vector3.zero;


        if (Input.GetKey(forwardKey))
            moveDirection += transform.forward;

        if (Input.GetKey(backwardKey))
            moveDirection -= transform.forward;

        if (Input.GetKey(leftKey))
            moveDirection -= transform.right;

        if (Input.GetKey(rightKey))
            moveDirection += transform.right;

        transform.position += movementSpeed * Time.deltaTime * moveDirection.normalized;
    }

    public void SetFreeView()
    {
        transform.position = freeViewPosition;
        transform.rotation = freeViewRotation;
        isFreeView = true;
    }

    public void SetUpperView()
    {
        transform.position = upperViewPosition;
        transform.rotation = upperViewRotation;
        isFreeView = false;
    }
}
