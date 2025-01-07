using System.Collections.Generic;
using UnityEngine;
using static MathMethods;

public class S_EnnemyTravel : MonoBehaviour
{
    internal List<Transform> path;
    Vector3 posToStart;
    Vector3 nextPos;
    int pathIndex = 0;
    float timer = 0;
    float speed;
    internal float traveledDistance;
    S_EnnemyBase ennemyScript;
    Vector3 originalPos;
    Vector3 NodeToGo;
    Vector2 direction;
    List<Vector3> travel = new List<Vector3>();
    public void Init()
    {
        ennemyScript=GetComponent<S_EnnemyBase>();
    }
    public void ReinitializePath(List<Transform> nodes, Vector3 firstPos, float enemySpeed)
    {
        path = nodes;
        originalPos = firstPos;
        pathIndex = 1;
        traveledDistance = 0;
        speed = enemySpeed;
        timer = 0;
        travel.Clear();
        foreach (Transform node in path) 
        {
            travel.Add(ChooseNode(node));
        }
        NodeToGo = travel[1];
        direction =(NodeToGo-originalPos).normalized;
    }
    void Update()
    {

        if (Vector3.Distance(transform.position, NodeToGo)<0.2f)
        {
            pathIndex += 1;
            if (pathIndex + 1 >= path.Count)
            {
                ennemyScript.ReducePlayerLives();
                pathIndex = 0;

            }
            else
            {
                originalPos=transform.position;
                NodeToGo = travel[pathIndex];
                direction = (NodeToGo - originalPos).normalized;
            }
        }
        Vector3 nextDirection= (Vector3)(direction * speed) * Time.deltaTime;
        transform.position += nextDirection;
        traveledDistance += nextDirection.magnitude;
    }
    private Vector3 ChooseNode(Transform nodeParent)
    {
        return nodeParent.GetChild(Random.Range(0, nodeParent.childCount)).position;
    }
}
