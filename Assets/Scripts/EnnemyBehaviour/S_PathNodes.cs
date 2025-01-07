using System.Collections.Generic;
using UnityEngine;

public class S_PathNodes : MonoBehaviour
{
    List<Transform> nodes = new();
    [SerializeField] int minDistanceBetweenNodes;

    [SerializeField]
    S_EnnemiesSpawner spawner;
    void Start()
    {
        nodes.Clear();
        nodes.Add(spawner.transform);
        for(int i=0; i<transform.childCount; i++)
        {
            Transform nextChild = transform.GetChild(i);
            for(int j=0; j<nextChild.childCount; j++)
            {
                nextChild.GetChild(j).localPosition=Random.insideUnitCircle;
            }
            if(Vector2.Distance(nextChild.position, nodes[i].position) != minDistanceBetweenNodes)
            {
                Vector3 nodeDirection=(nextChild.position - nodes[i].position).normalized;
                Vector3 newChildPos=nodes[i].position+nodeDirection*minDistanceBetweenNodes;
                nextChild.transform.position = newChildPos;
            }
            nodes.Add(nextChild);
        }
        spawner.GetNodes(nodes);
    }
}
