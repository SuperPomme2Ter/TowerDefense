using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static MathMethods;

public class S_TowerBase : S_ClickableObject, IpoolInterface<S_TowerBase>
{

    public Dictionary<Stats, float> stats = new Dictionary<Stats, float>();
    Dictionary<ResourceType, int> cost = new Dictionary<ResourceType, int>();

    List<S_EnnemyBase> enemiesInRange= new();

    bool Hitscan = false;
    bool AOE = false;
    bool arching = false;

    internal S_ModuleInventory moduleInventory;

    [SerializeField] float AOERange;
    [SerializeField] float velocity;

    [SerializeField] SO_Towers towerCharacteristics;
    
    private System.Func<Vector2, Vector2, float, Vector2> projectileTrajectory;

    S_Pool<S_ProjectileBase> projectilePool;

    Action<S_EnnemyBase> statusToInflict;

    SpriteRenderer baseSprite;

    S_TowerCooldown cooldown;
    private S_CanonRotation canonCharacteristics;
    

    GameObject range;
    S_Pool<S_TowerBase> pool;
    [SerializeField]
    float archingPower;

    ActiveModifiers[] installedActiveModules = new ActiveModifiers[4];

    private void Start()
    {
        Init();

    }
    internal void Init()
    {
        projectilePool= transform.parent.parent.GetComponentInChildren<S_ProjectileSpawner>().GetProjectilePool();
        baseSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        cooldown = GetComponent<S_TowerCooldown>();
        canonCharacteristics=transform.GetChild(2).GetComponent<S_CanonRotation>();
        range = transform.GetChild(0).gameObject;
        SetSO(towerCharacteristics);



    }
    private bool Fire()
    {
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange.OrderBy(x => x.GetTravelledDistance()).ToList();
            S_ProjectileBase projectileToLaunch = LaunchProjectile(enemiesInRange[0].transform.position);
            
            return true;

        }
        else
        {
            return false;
        }
        
    }
    private S_ProjectileBase LaunchProjectile( Vector2 endTravel)
    {
        Vector3 fireDirection=projectileTrajectory.Invoke(transform.position,endTravel,Time.deltaTime);
        canonCharacteristics.AlignToFire(fireDirection);
        S_ProjectileBase launchedProjectile = projectilePool.Get();
        launchedProjectile.damage = UnityEngine.Random.Range((int)stats[Stats.DamageMin],(int) stats[Stats.DamageMax]);
        launchedProjectile.velocity = velocity;
        launchedProjectile.parentTower = this;
        launchedProjectile.sprite.sprite = towerCharacteristics.projectileSprite;
        launchedProjectile.AOERange = AOERange;
        launchedProjectile.statusFunction = statusToInflict;
        launchedProjectile.trajectory = projectileTrajectory;
        launchedProjectile.StopAllCoroutines();
        launchedProjectile.StartCoroutine(launchedProjectile.Travel(canonCharacteristics.firingPosition.position,endTravel));


        return launchedProjectile;


    }
    private void ActualizeCooldown()
    {
        cooldown.firing = Fire;
        cooldown.delay = stats[Stats.atkSpeed];
        Fire();
    }
    private void SetProjectileStats()
    {
        if (arching)
        {
            projectileTrajectory = (originalPos, hit, timer) => Bezier(originalPos, (originalPos + hit) / 2 + new Vector2(0, archingPower), hit, timer);
        }
        else
        {
            projectileTrajectory = (originalPos,hit, timer) => Bezier(originalPos, hit, timer);
        }
        ActualizeCooldown();
    }
    private void ReceiveModule()
    {
        if (moduleInventory.selectedModule == null)
        {
            return;
        }
        moduleInventory.selectedModule.ApplyModifier(this);
        moduleInventory.RemoveModule();
        ActualizeCooldown();
    }
    public void SetSO(SO_Towers statsToGive)
    {
        towerCharacteristics = statsToGive;
        stats=towerCharacteristics.stats;
        velocity= statsToGive.velocity;
        AOERange=statsToGive.AOERange;
        arching=statsToGive.arching;
        archingPower = statsToGive.archingPower;
        baseSprite.sprite = statsToGive.baseSprite;
        GetComponent<CircleCollider2D>().radius = stats[Stats.Range];
        range.transform.localScale = new Vector2(stats[Stats.Range] * 2, stats[Stats.Range] * 2);
        canonCharacteristics = statsToGive.canonCharacteristics.GetComponent<S_CanonRotation>();
        SetProjectileStats();

    }

    public void SetPool(S_Pool<S_TowerBase> _pool)
    {
        pool = _pool;
    }

    public void SetActive(bool active)
    {
        throw new NotImplementedException();
    }
    public override void Selected()
    {
        Debug.Log("aaaaaaaa");
        ReceiveModule();
    }

    public override void Unselected()
    {
        Debug.Log("Tower LoseFocus");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        enemiesInRange.Add(other.GetComponent<S_EnnemyBase>());
        enemiesInRange.OrderBy(x => x.GetTravelledDistance()).ToList();
        if (!cooldown.enabled)
        {
            ActualizeCooldown();
            cooldown.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enemiesInRange.Remove(other.GetComponent<S_EnnemyBase>());
    }

    
}
