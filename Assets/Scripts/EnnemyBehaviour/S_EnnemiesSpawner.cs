using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class S_EnnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ennemyPrefab;
    [FormerlySerializedAs("ressourceCounter")] [SerializeField] private S_RessourceManager ressourceManager;
    public float pathHeight = 0.5f;
    [SerializeField] private List<SO_Ennemy> _EnnemyType;



    private S_Pool<S_EnnemyBase> _ennemyPool;
    List<Transform> nodes;


    private void Awake()
    {
        _ennemyPool= new S_Pool<S_EnnemyBase>(CreateEnnemy, OnGetEnnemy, OnReleaseEnnemy,100,50);
    }

    public void GetNodes(List<Transform> path)
    {
        nodes = path;
    }
    public void SpawnEnnemy(int index)
    {
        S_EnnemyBase newEnnemy= _ennemyPool.Get();
        newEnnemy.GetStats(nodes, _EnnemyType[index]);
    }

    private S_EnnemyBase CreateEnnemy()
    {
        GameObject go = Instantiate(_ennemyPrefab, transform);
        S_EnnemyBase ennemy = go.GetComponent<S_EnnemyBase>();
        ennemy.Init(ressourceManager);
        return ennemy;
    }
    private void OnGetEnnemy(S_EnnemyBase ennemy)
    {
        ennemy.gameObject.SetActive(true);
        ennemy.transform.position=transform.position + ((Vector3) Random.insideUnitCircle  * pathHeight);
        
    }

    private void OnReleaseEnnemy(S_EnnemyBase ennemy)
    {
        ennemy.gameObject.SetActive(false);
    }
}
