using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 targetPos;
    [SerializeField] private float smoothTime = 1F;
    bool shouldMoveUp;
    bool shouldMoveDown;

    [SerializeField] private Transform push;

    void Start()
    {
        originalPos = push.position;
        targetPos = originalPos + new Vector3(0, -0.2f, 0);
    }

    void Update()
    {
        if (shouldMoveDown)
            MoveDown();
        else if (shouldMoveUp)
            MoveUp();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            shouldMoveDown = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            shouldMoveUp = true;
        }
    }

    void MoveDown()
    {
        push.position = Vector3.Lerp(originalPos, targetPos, smoothTime);
        if (push.transform.position == targetPos)
            shouldMoveDown = false;
    }
    void MoveUp()
    {
        if (!Physics2D.IsTouchingLayers(GetComponent<Collider2D>()))
        {
            push.position = Vector3.Lerp(targetPos, originalPos, smoothTime);
            if (push.transform.position == originalPos)
                shouldMoveUp = false;
        }
    }
}
