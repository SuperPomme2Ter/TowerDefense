using UnityEngine;

public class S_ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    private S_Pool<S_ProjectileBase> _projectilePool;

    private void Awake()
    {
        _projectilePool = new S_Pool<S_ProjectileBase>(CreateProjectile, OnGetProjectile, OnReleaseProjectile, 200, 50);
    }

    private S_ProjectileBase CreateProjectile()
    {
        GameObject go = Instantiate(_projectilePrefab, transform);
        S_ProjectileBase projectile = go.GetComponent<S_ProjectileBase>();
        projectile.Init();
        return projectile;
    }
    private void OnGetProjectile(S_ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.velocity = 0;
        projectile.AOERange = 0;
        projectile.damage = 0;
        projectile.parentTower = null;
        projectile.trajectory = null;
        projectile.StopAllCoroutines();
    }
    private void OnReleaseProjectile(S_ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.velocity = 0;
        projectile.AOERange = 0;
        projectile.damage = 0;
        projectile.parentTower = null;
        projectile.trajectory = null;
        projectile.StopAllCoroutines();
    }
    public S_Pool<S_ProjectileBase> GetProjectilePool()
    {
        return _projectilePool;
    }  
}
