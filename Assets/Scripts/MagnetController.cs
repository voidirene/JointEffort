using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [SerializeField] private string inputAxisName = "UseMagnet";

    [SerializeField] private MagnetPoint magnetPoint;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw(inputAxisName) != 0)
        {
            magnetPoint.HoldObject();
        }
        else
        {
            magnetPoint.DropObject();
        }
    }
}
