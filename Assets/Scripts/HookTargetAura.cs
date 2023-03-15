using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script handles the aura that appears when a player approaches the hook point
public class HookTargetAura : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform detectedPlayer;

    Color colorMin;
    Color colorMax;

    void Awake()
    {
        //set the min and max values of the colors
        colorMin = colorMax = Color.white;
        colorMin.a = 0f;
        colorMax.a = 20f/255f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorMin;
    }

    void Update()
    {
        if (detectedPlayer != null)
        {
            LerpColor();
        }
    }

    void LerpColor()
    {
        float distance = Vector3.Distance(detectedPlayer.transform.position, transform.position)/10;
        spriteRenderer.color = Color.Lerp(colorMax, colorMin, distance);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (detectedPlayer == null && other.CompareTag("Player"))
        {
            detectedPlayer = other.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        detectedPlayer = null;
    }
}
