using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_ProjectileBase : MonoBehaviour, IpoolInterface<S_ProjectileBase>
{
    
    public Action<S_EnnemyBase> statusFunction;
    public float AOERange;
    public int damage;
    public float velocity;
    public SpriteRenderer sprite;
    S_Pool<S_ProjectileBase> pool;
    internal Vector3 originPos;
    internal Vector3 targetPos;
    private float timer = 0;
    internal ProjectileBehaviour behaviour;
    //public S_TowerBase parentTower;

    internal void Init()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void DamageEnnemies(List<S_EnnemyBase> enemies)
    {
        if (AOERange < 0.1f && enemies.Count>0) { enemies.First().ReceiveDamage(damage); }
        foreach (S_EnnemyBase enemy in enemies)
        {
            enemy.ReceiveDamage(damage);
        }
        pool.Release(this);
    }
    private void Update()
    {
        transform.position=behaviour.TrajectoryFunction.Invoke(originPos, targetPos, timer);
        transform.right = (Vector3) behaviour.TrajectoryFunction.Invoke(originPos, targetPos, timer) - transform.position; //Evil LookAt2D
        timer += Time.deltaTime*behaviour.constFloatVariables[0];
        if(behaviour.VerifyEndTrajectory.Invoke(transform.position,timer))
        {
            transform.position=targetPos;
            List<Collider2D> colliders = behaviour.EnemiesDetectionFunction.Invoke();
            List<S_EnnemyBase> enemiesInDamage = new();
            if (colliders.Count > 0)
            {
                colliders.ForEach(collider =>enemiesInDamage.Add( collider.GetComponent<S_EnnemyBase>()));

                DamageEnnemies(enemiesInDamage);
            }
            else
            {
                pool.Release(this);
            }
        }
    }

    // public void Travel(Vector2 trajectoryBeginning , Vector2 hit)
    // {
    //     Vector2 originalPos= trajectoryBeginning;
    //     while (true)
    //     {
    //         
    //         transform.position = trajectory.Invoke(originalPos,hit, timer);
    //         transform.right = (Vector3) trajectory.Invoke(originalPos, hit, timer+Time.deltaTime) - transform.position; //Evil LookAt2D
    //
    //         timer += Time.deltaTime*velocity;
    //         if(timer > 0.98f)
    //         {
    //             transform.position=hit;
    //             break;
    //         }
    //
    //     }
    //
    //     List<Collider2D> colliders=new List<Collider2D>();
    //     if (AOE)
    //     {
    //         colliders = Physics2D.OverlapCircleAll(hit, AOERange, (1<<8)).ToList();
    //     }
    //     else
    //     {
    //         colliders.Clear();
    //         colliders.Add(Physics2D.OverlapCircle(hit, AOERange, (1<<8)));
    //     }
    //     
    //     List<S_EnnemyBase> enemiesInDamage = new();
    //     if (colliders.Count > 0)
    //     {
    //         colliders.ForEach(collider =>enemiesInDamage.Add( collider.GetComponent<S_EnnemyBase>()));
    //
    //         DamageEnnemies(enemiesInDamage);
    //     }
    //     else
    //     {
    //         pool.Release(this);
    //     }
    // }

    public void SetPool(S_Pool<S_ProjectileBase> _pool)
    {
        pool = _pool;
        if (pool == null)
        {
            Debug.Log("wtf");
        }
    }
}
