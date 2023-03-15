using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HookRope : MonoBehaviour
{

    public Vector2 target;

    //the speed at which the hook will travel to reach the target. might not be necessary TODO
    [SerializeField] private float speed = 1;

    [SerializeField] private GameObject nodePrefab;
    //distance between rope nodes
    [SerializeField] private float distance = 2;

    //need a reference to the player transform in order to connect them to one end of the rope
    public Transform playerTransform;
    private Transform lastNode;

    //private int maxNodes;

    private LineRenderer lr;
    private int vertexCount = 2;
    [SerializeField] private List<Transform> Nodes = new List<Transform>();

    private bool done = false;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();

        lastNode = transform;
        Nodes.Add(transform);
    }

    void Update() //TODO: try fixed or late update
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed);

        if (done == false && (Vector2)transform.position != target)
        {
            if (Vector2.Distance(playerTransform.position, lastNode.position) > distance)
            {
                CreateNode();
            }
        }
        else if (done == false)
        {
            done = true;
            while (Vector2.Distance(playerTransform.position, lastNode.position) > distance) //allows the rest of the nodes to be created even after the hook reaches its target
            {
                CreateNode();
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody = playerTransform.GetComponent<Rigidbody2D>();
            lastNode.GetComponent<DistanceJoint2D>().connectedBody = playerTransform.GetComponent<Rigidbody2D>();

            StartCoroutine(LockJoints());
        }

        RotateNodes();
        RenderLine();
    }

    void CreateNode()
    {
        Vector2 creationPos = playerTransform.position - lastNode.position;
        creationPos.Normalize();
        creationPos *= distance;
        creationPos += (Vector2)lastNode.position;

        GameObject go = (GameObject)Instantiate(nodePrefab, creationPos, Quaternion.identity);
        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        lastNode.GetComponent<DistanceJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        lastNode = go.transform;

        Nodes.Add(lastNode);

        vertexCount++;
    }

    void RenderLine()
    {
        lr.positionCount = vertexCount;

        int i;
        for (i = 0; i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position);
        }

        lr.SetPosition(i, playerTransform.position);
    }

    void RotateNodes()
    {
        for (int i = 1; i < Nodes.Count; i++)
        {
            Transform node = Nodes[i];
            Transform sprite = node.GetChild(0);
            Transform prevNode = Nodes[i - 1];
            Vector3 directionToPrev = prevNode.position - node.position;
            float angleToPrev = Mathf.Atan2(directionToPrev.y, directionToPrev.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angleToPrev));
            sprite.rotation = targetRotation;
        }

        // Handle Hook separately
        Transform hook = Nodes[0];
        Transform hookSprite = hook.GetChild(0);
        Vector3 directionToTarget = (Vector3)target - hook.position;
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetHookRotation = Quaternion.Euler(new Vector3(0, 0, angleToTarget));
        hookSprite.rotation = targetHookRotation;
    }

    private void RotateNodesnope()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Vector3 targetPos;
            if (i == 0)
            {
                targetPos = target;
            }
            else
            {
                targetPos = Nodes[i - 1].GetChild(0).position;
            }

            Vector3 direction = targetPos - Nodes[i].GetChild(0).position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Nodes[i].GetChild(0).rotation = Quaternion.Slerp(Nodes[i].GetChild(0).rotation, rotation, 5f * Time.deltaTime);
        }
    }

    private IEnumerator LockJoints() //to prevent stretching over time
    {
        yield return new WaitForSeconds(1);
        for (int i = 1; i < Nodes.Count; i++)
        {
            Nodes[i].GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
            Nodes[i].GetComponent<DistanceJoint2D>().autoConfigureDistance = false;
        }
    }
}