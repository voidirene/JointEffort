using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagnetController : MonoBehaviour
{
    [SerializeField] private string inputAxisName = "UseMagnet";

    [SerializeField] private MagnetPoint magnetPoint;
    
    //for enabling/disabling hook swinging
    [SerializeField] private UnityEvent isHolding;
    [SerializeField] private UnityEvent isNotHolding;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw(inputAxisName) != 0)
        {
            magnetPoint.HoldObject();
            isHolding.Invoke();
        }
        else
        {
            magnetPoint.DropObject();
            isNotHolding.Invoke();
        }
    }
}
