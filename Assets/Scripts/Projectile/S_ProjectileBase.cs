using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_ProjectileBase : MonoBehaviour, IpoolInterface<S_ProjectileBase>
{
    
    public Action<S_EnnemyBase> statusFunction;
    public System.Func< Vector2,Vector2,float,Vector2> trajectory;
    public float AOERange;
    public int damage;
    public float velocity;
    public SpriteRenderer sprite;
    S_Pool<S_ProjectileBase> pool;
    public S_TowerBase parentTower;

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

    public IEnumerator Travel(Vector2 trajectoryBeginning , Vector2 hit)
    {
        Vector2 originalPos= trajectoryBeginning;
        float timer = 0;
        while (true)
        {
            
            transform.position = trajectory.Invoke(originalPos,hit, timer);
            transform.right = (Vector3) trajectory.Invoke(originalPos, hit, timer+Time.deltaTime) - transform.position; //Evil LookAt2D

            timer += Time.deltaTime*velocity;
            yield return new WaitForEndOfFrame();
            if(timer > 0.98f)
            {
                transform.position=hit;
                break;
            }

        }
        List<Collider2D> colliders = Physics2D.OverlapCircleAll(hit, AOERange, (1<<8)).ToList();
        List<S_EnnemyBase> enemiesInDamage = new();

        colliders.ForEach(collider =>enemiesInDamage.Add( collider.GetComponent<S_EnnemyBase>()));

        DamageEnnemies(enemiesInDamage);
        timer = 0;
        yield return null;
    }

    public void SetPool(S_Pool<S_ProjectileBase> _pool)
    {
        pool = _pool;
        if (pool == null)
        {
            Debug.Log("wtf");
        }
    }
}
