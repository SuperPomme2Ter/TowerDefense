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

        //projectile.parentTower = null;
    }
    private void OnReleaseProjectile(S_ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.damage = 0;
        projectile.originPos=Vector3.zero;
        projectile.targetPos=Vector3.zero;
        //projectile.parentTower = null;
        projectile.StopAllCoroutines();
    }
    public S_Pool<S_ProjectileBase> GetProjectilePool()
    {
        return _projectilePool;
    }  
}
