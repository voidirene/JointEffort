using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script tells the HookThrow scripts attached to the players if they're in range to attach to this hooktarget
public class HookTarget : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponentInChildren<HookThrow>().detectedHooks.Add(transform);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponentInChildren<HookThrow>().detectedHooks.Remove(transform);
        }
    }
}
