using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinjingi;

public class HookThrow : MonoBehaviour {

	public List<Transform> detectedHooks = new List<Transform>();
	private bool ropeActive = false;
	[SerializeField] private GameObject hookPoint;
	private GameObject currentRope;

    //player's movement scripts
    private Move moveScript;
    private Jump jumpScript;
    private RopeMovement ropeMovementScript;
    [SerializeField] private GameObject magnet;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        moveScript = player.GetComponent<Move>();
        jumpScript = player.GetComponent<Jump>();
        ropeMovementScript = player.GetComponent<RopeMovement>();
        ropeMovementScript.enabled = false;
    }
	
    public void ThrowHook()
    {
		if (detectedHooks.Count != 0 && ropeActive == false) 
        {
			Vector2 target = FindClosestHookPoint().position;

            currentRope = (GameObject)Instantiate(hookPoint, transform.position, Quaternion.identity);
            HookRope hookRope = currentRope.GetComponent<HookRope>();
            hookRope.target = target;
            hookRope.playerTransform = player.transform;
			ropeActive = true;
            DisableMovementScripts();
		} 
    }

    public void DestroyHook()
    {
		Destroy(currentRope);
		ropeActive = false;
        EnableMovementScripts();
    }

    private Transform FindClosestHookPoint()
    {
        Transform closestHook = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform hook in detectedHooks)
        {
            float distance = Vector2.Distance(hook.position, currentPosition);
            if (distance < minDistance)
            {
                closestHook = hook;
                minDistance = distance;
            }
        }

        return closestHook;
    }

    private void EnableMovementScripts()
    {
        moveScript.enabled = true;
        jumpScript.enabled = true;
        ropeMovementScript.enabled = false;
        magnet.SetActive(true);
    }
    private void DisableMovementScripts()
    {
        moveScript.enabled = false;
        jumpScript.enabled = false;
        ropeMovementScript.enabled = true;
        magnet.SetActive(false);
    }
}