using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private List<string> acceptedTags;

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in acceptedTags)
        {
            if (other.CompareTag(tag))
            {
                GameObject.Find("MenuManager").GetComponent<MenuManager>().GameLose();
            }
        }
    }
}
