using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TowerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private S_ModuleInventory moduleInventory;
    internal S_TowerBase towerToApplyStat;

    private S_Pool<S_TowerBase> _towerPool;

    private void Awake()
    {
        _towerPool = new S_Pool<S_TowerBase>(CreateTower, OnGetTower, OnReleaseTower,20, 5);
    }

    private S_TowerBase CreateTower()
    {
        GameObject go = Instantiate(_towerPrefab, transform);
        S_TowerBase tower = go.GetComponent<S_TowerBase>();
        tower.Init();
        return tower;
    }
    private void OnGetTower(S_TowerBase tower)
    {
        tower.gameObject.SetActive(true);
    }

    private void OnReleaseTower(S_TowerBase tower)
    {
        tower.gameObject.SetActive(false);
    }
    public S_TowerBase SpawnTower(SO_Towers stats)
    {
       S_TowerBase newTower= _towerPool.Get();
       newTower.SetSO(stats);
        return newTower;
    }
}
