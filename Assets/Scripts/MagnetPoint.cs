using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    [SerializeField] private List<string> acceptedTags; 

    private Rigidbody2D detectedObject;
    private Rigidbody2D heldObject;

    public void HoldObject()
    {
        if (detectedObject != null && heldObject == null)
        {
            detectedObject.transform.SetParent(transform);
            heldObject = GetComponentInChildren<Rigidbody2D>();
            heldObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (heldObject != null)
        {
            heldObject.bodyType = RigidbodyType2D.Kinematic;
            heldObject.transform.localPosition = Vector3.zero;
        }
    }
    public void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<BoxCollider2D>().enabled = true;
            heldObject.bodyType = RigidbodyType2D.Dynamic;
            heldObject = null;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (heldObject == null)
        {
            foreach (string tag in acceptedTags)
            {
                if (other.CompareTag(tag))
                    detectedObject = other.GetComponent<Rigidbody2D>();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        detectedObject = null;
    }
}
