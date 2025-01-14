using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MathMethods;

public class S_TowerBase : S_ClickableObject, IpoolInterface<S_TowerBase>
{

    public Dictionary<Stats, float> stats = new Dictionary<Stats, float>();
    Dictionary<ResourceType, int> cost = new Dictionary<ResourceType, int>();

    internal List<S_EnnemyBase> enemiesInRange= new();

    bool Hitscan = false;
    bool AOE = false;
    bool arching = false;

    internal S_ApplyModuleToTower moduleApplier;
    internal S_ModuleHUDInteraction moduleHUD;

    [SerializeField] float AOERange;
    [SerializeField] float velocity;

    [SerializeField] SO_Towers towerCharacteristics;
    
    private System.Func<Vector2, Vector2, float, Vector2> projectileTrajectory;

    S_Pool<S_ProjectileBase> projectilePool;

    Action<S_EnnemyBase> statusToInflict;

    internal S_TowerCooldown cooldown;
    
    private System.Func<Vector3,int,bool> canonFiringFunction;
    private Transform canonFiringPosition;
    private int canonChildIndex;
    

    [SerializeField] GameObject rangeCollider;
    [SerializeField] GameObject rangeSprite;
    S_Pool<S_TowerBase> pool;
    [SerializeField]
    float archingPower;
    [SerializeField] GameObject baseGameobject;

    private GameObject canon;
    ActiveModifiers[] installedActiveModules = new ActiveModifiers[4];

    private void Start()
    {
        Init();

    }
    internal void Init()
    {
        canonFiringPosition = gameObject.transform;
        projectilePool= transform.parent.parent.GetComponentInChildren<S_ProjectileSpawner>().GetProjectilePool();
        cooldown = GetComponent<S_TowerCooldown>();
        moduleApplier = transform.parent.GetComponent<S_ApplyModuleToTower>();
        moduleHUD=moduleApplier.moduleHUD;
        cooldown.firing = Fire;
        SetSO(towerCharacteristics);



    }
    private bool Fire()
    {
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange.OrderBy(x => x.GetTravelledDistance()).ToList();
            TriggerProjectileBehaviour(enemiesInRange[0].transform.position);
            
            return true;

        }
        else
        {
            return false;
        }
        
    }
    private void TriggerProjectileBehaviour( Vector2 endTravel)
    {

        Vector3 fireDirection=projectileTrajectory.Invoke(transform.position,endTravel,1);
        if (!canonFiringFunction.Invoke(fireDirection, canonChildIndex))
        {
            return;
        }
        LaunchProjectileSafe(endTravel);


    }
    internal void LaunchProjectileSafe(Vector2 endTravel)
    {
        S_ProjectileBase launchedProjectile = projectilePool.Get();
        launchedProjectile.damage = UnityEngine.Random.Range((int)stats[Stats.DamageMin],(int) stats[Stats.DamageMax]);
        launchedProjectile.velocity = velocity;
        launchedProjectile.parentTower = this;
        launchedProjectile.sprite.sprite = towerCharacteristics.projectileSprite;
        launchedProjectile.AOERange = AOERange;
        launchedProjectile.statusFunction = statusToInflict;
        launchedProjectile.trajectory = projectileTrajectory;
        launchedProjectile.StopAllCoroutines();
        launchedProjectile.StartCoroutine(launchedProjectile.Travel(canonFiringPosition.position,endTravel));
    }
    private void ActualizeCooldown()
    {
        cooldown.delay = stats[Stats.atkSpeed];
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
    public void SetSO(SO_Towers statsToGive)
    {
        towerCharacteristics = statsToGive;
        stats=towerCharacteristics.stats;
        velocity= statsToGive.velocity;
        AOERange=statsToGive.AOERange;
        arching=statsToGive.arching;
        archingPower = statsToGive.archingPower;
        canon=transform.GetChild(statsToGive.canonChildIndex).gameObject;
        canon.SetActive(true);
        rangeCollider.GetComponent<CircleCollider2D>().radius = stats[Stats.Range];
        rangeSprite.transform.localScale = new Vector2(stats[Stats.Range]*2, stats[Stats.Range]*2);
        canonChildIndex = statsToGive.canonChildIndex;
        S_CanonCharacteristics canonScript = baseGameobject.transform.GetChild(canonChildIndex).GetComponent<S_CanonCharacteristics>();
        canonScript.gameObject.SetActive(true);
        canonFiringFunction=canonScript.FiringFunctions[canonChildIndex];
        canonFiringPosition= canonScript.offsetFiringPosition;
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
        Debug.Log("Selected");
        Debug.Log(this.gameObject.name);
        if (moduleHUD.GiveModule(out S_ModuleBase givedModule))
        {
            stats=moduleApplier.TryApplyModule(stats,givedModule);
            ActualizeCooldown();
        }
        rangeSprite.SetActive(true);
    }

    public override void Unselected()
    {
        Debug.Log("Tower LoseFocus");
        rangeSprite.SetActive(false);
    }
    internal void AddEnemyInRange(Collider2D other)
    {
        enemiesInRange.Add(other.GetComponent<S_EnnemyBase>());
        enemiesInRange.OrderBy(x => x.GetTravelledDistance()).ToList();
        if (!cooldown.enabled)
        {
            ActualizeCooldown();
            cooldown.enabled = true;
        }
    }
    internal void RemoveEnemyInRange(Collider2D other)
    {
        enemiesInRange.Remove(other.GetComponent<S_EnnemyBase>());
    }
    
}
