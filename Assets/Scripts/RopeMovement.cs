using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    [SerializeField] private string horizontalAxis = "Horizontal";
    //[SerializeField] private string VerticalAxis = "Vertical";

    [SerializeField] private float swingForce = 200;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(horizontalAxis);

        if (horizontalInput != 0)
        {
            rb.AddForce(Vector2.right * swingForce * horizontalInput * Time.fixedDeltaTime);
        }
    }
}
