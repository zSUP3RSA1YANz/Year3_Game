using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    [Range(2, 12)]
    private float speed = 4;

    private Vector3 targetPosition;
    private bool isMoving = true;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            setTargetPosition();
        }

        if(isMoving)
        {
            move();
        }
    }

    void setTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        targetPosition.y = transform.position.y;

        isMoving = true;
    }

    void move()
    {
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
