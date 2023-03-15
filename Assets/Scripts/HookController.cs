using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HookController : MonoBehaviour
{
    [SerializeField] private string inputAxisName = "UseHook";

    [SerializeField] private HookThrow hookThrow;
    
    //for enabling/disabling magnet holding
    [SerializeField] private UnityEvent isSwinging;
    [SerializeField] private UnityEvent isNotSwinging;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw(inputAxisName) != 0)
        {
            hookThrow.ThrowHook();
            isSwinging.Invoke();
        }
        else
        {
            hookThrow.DestroyHook();
            isNotSwinging.Invoke();
        }
    }
}
