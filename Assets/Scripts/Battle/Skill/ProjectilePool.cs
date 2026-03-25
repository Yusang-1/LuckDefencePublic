using UnityEngine;

public class ProjectilePool
{
    private Projectile[] pool;
    private int poolSize;
    
    public ProjectilePool(ShootProjectileSkillEffect data)
    {
        poolSize = data.PoolCount;
        pool = new Projectile[poolSize];
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Object.Instantiate(data.ProjectilePrefab);
            obj.SetActive(false);
            pool[i] = obj.GetComponent<Projectile>();
        }
    }
    
    public Projectile GetProjectile()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        
        Debug.LogWarning("모든 투사체 사용중");
        return null;
    }
}
