using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_EnnemyBase : S_ClickableObject, IpoolInterface<S_EnnemyBase>
{
    internal S_Pool<S_EnnemyBase> pool;
    public int pv;
    float speed;
    int ressourceGiven;
    S_RessourceManager _ressourceManager;
    Func<int> status;
    Vector3 ObjectivePos;
    S_EnnemyTravel pathToTravel;
    Sprite ennemyRenderer;
    public void Init(S_RessourceManager manager)
    {
        pathToTravel=gameObject.GetComponent<S_EnnemyTravel>();
        ennemyRenderer=GetComponentInChildren<SpriteRenderer>().sprite;
        _ressourceManager = manager;
        pathToTravel.Init();

    }


    public void ReceiveDamage(int damage)
    {
        pv-=damage;
        verifyPV();
    }
    private void verifyPV()
    {
        if (pv <= 0)
        {
            pool.Release(this);
            _ressourceManager.resourcesQuantity[ResourceType.Base] += ressourceGiven;
            _ressourceManager.UpdateResourceDisplay();
        }
    }

    internal void ReducePlayerLives()
    {
        pool.Release(this);
    }
    public void GetStats(List<Transform> _nodes, SO_Ennemy _baseStats)
    {
        pv= _baseStats.pv;
        speed=_baseStats.Speed;
        ennemyRenderer=_baseStats.sprite;
        ressourceGiven= _baseStats.ressourceGiven;
        List<Transform> nodesPos = new List<Transform>();
        nodesPos.Clear();
        foreach (Transform t in _nodes) 
        {
            nodesPos.Add(t);
        }
        pathToTravel.ReinitializePath(nodesPos, transform.position, speed);
    }
    public float GetTravelledDistance()
    {
        return pathToTravel.traveledDistance;
    }

    public void SetPool(S_Pool<S_EnnemyBase> _pool)
    {
        pool = _pool;
    }
    public void SetRessourceVariable()
    {

    }

    public override void Selected()
    {
        Debug.Log("aaaa");
    }

    public override void Unselected()
    {
        Debug.Log("bbbb");
    }
}
